using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aidon.Tools.Gollum.Bugzilla;
using Aidon.Tools.Gollum.ReviewBoard;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum
{
    /// <summary>
    /// Callback for asking credentials from user if no cookie exists.
    /// </summary>
    /// <param name="title">The title of the credentials popup window.</param>
    /// <returns>
    /// User credentials
    /// </returns>
    public delegate Credentials CredentialCallback(string title);

    /// <summary>
    /// Delegate for sending update messages back to the user interface.
    /// </summary>
    /// <param name="message">The message.</param>
    public delegate void Update(string message);

    public class GollumEngine : Progress<string>
    {
        private readonly SubversionArguments _subversionArguments;
        private readonly ProjectSettings _projectSettings;

        private readonly IPatchCreator _patchCreator;
        private readonly IReviewBoardHandler _reviewBoardHandler;
        private readonly IBugzillaHandler _bugzillaClient;

        public event CredentialCallback CredentialsCallback;
        public event EventHandler<CopyToClipboardEventArgs> CopyToClipboard;

        public GollumEngine(SubversionArguments subversionArguments, ProjectSettings projectSettings)
        {
            var reviewBoardUrl = ConfigurationManager.AppSettings["ReviewBoardUrl"];

            if (String.IsNullOrEmpty(reviewBoardUrl))
            {
                throw new InvalidOperationException("ReviewBoard url is required.");
            }
            
            _projectSettings = projectSettings;
            _subversionArguments = subversionArguments;

#if TEST
            _patchCreator = new DummyPatchCreator();
            _reviewBoardHandler = new DummyReviewBoardHandler();
#if !NOBUGZILLA
            _bugzillaClient = new DummyBugzillaRestClient();
#else
            _bugzillaClient = null;
#endif
#else
            _patchCreator = new SvnPatchCreator();
            _reviewBoardHandler = new ReviewBoardRestClient(reviewBoardUrl);
            var bugzillaUrl = ConfigurationManager.AppSettings["BugzillaUrl"];
            if (!String.IsNullOrEmpty(bugzillaUrl))
            {
                _bugzillaClient = new BugzillaRestClient(bugzillaUrl);
            }
#endif
        }

        /// <summary>
        /// The SVN commit message.
        /// </summary>
        public string CommitMessage
        {
            get { return _subversionArguments.Message; }
        }

        /// <summary>
        /// The SVN commit revision from which the diff starts.
        /// </summary>
        public int CommitRevisionFrom
        {
            get { return _subversionArguments.RevisionFrom; }
        }

        /// <summary>
        /// The SVN commit revision to which the diff end.
        /// </summary>
        public int CommitRevisionTo
        {
            get { return _subversionArguments.RevisionTo; }
        }

        /// <summary>
        /// Indicates whether the bugzilla integration is enabled.
        /// </summary>
        public bool BugzillaEnabled
        {
            get { return _bugzillaClient != null; }
        }

        /// <summary>
        /// The review board repository.
        /// </summary>
        public string ReviewBoardRepositoryName
        {
            get { return _projectSettings.ReviewBoardRepositoryName; }
        }

        /// <summary>
        /// The SVN repository base path
        /// </summary>
        public string RepositoryBasePath
        {
            get { return _projectSettings.RepositoryBasePath; }
        }

        /// <summary>
        /// Posts a review ticket to review board.
        /// </summary>
        /// <param name="summary">The review summary.</param>
        /// <param name="description">The review description.</param>
        /// <param name="bugs">The review fixed bugs.</param>
        /// <returns>
        /// True if posting to review board succeeds; otherwise, false.
        /// </returns>
        public async Task PostToReviewBoardAsync(string summary, string description, string bugs)
        {
            try
            {
                OnReport("Creating diff...");

                string patch = await _patchCreator.CreatePatchAsync(_subversionArguments);

                OnReport("Diff created. Creating review ticket...");

                var arguments = new ReviewBoardArguments
                {
                    CredentialCallback = CredentialCallback,
                    Description = description,
                    Group = _projectSettings.ReviewBoardGroup,
                    BaseDirectory = FigureOutRepositoryPath(),
                    Repository = _projectSettings.ReviewBoardRepositoryName,
                    Summary = summary,
                    Bugs = bugs,
                    DiffFile = patch
                };

                var response = await _reviewBoardHandler.PostToReviewBoardAsync(arguments).ConfigureAwait(false);
                OnCopyToClipboard(response.ReviewUrl);

                OnReport("Uploading diff...");
                await _reviewBoardHandler.UploadDiffAsync(response.ReviewRequest, arguments).ConfigureAwait(false);

                OnReport("Diff uploaded...");
            }
            catch (ReviewBoardAuthenticationException)
            {
                throw;
            }
            catch (ReviewBoardException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Posts a bug update to bugzilla.
        /// </summary>
        /// <param name="bugNumber">The bug number.</param>
        /// <param name="resolution">The resolution.</param>
        /// <param name="status">The status.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="updateToken">The update token.</param>
        /// <returns></returns>
        public async Task PostToBugzillaAsync(string bugNumber, string resolution, string status, string comment, string updateToken)
        {
            OnReport("Updating Bugzilla...");
            var arguments = new BugzillaArguments
            {
                BugId = bugNumber,
                UpdateToken = updateToken,
                Comment = comment,
                Resolution = resolution,
                Status = status
            };

            await _bugzillaClient.PostToBugzillaAsync(arguments).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the bug information.
        /// </summary>
        /// <param name="bugNumber">The bug number.</param>
        /// <param name="tokenSource">The cancel token source.</param>
        /// <returns>
        /// Bugzilla bug information.
        /// </returns>
        public async Task<BugzillaBug> GetBugInformationAsync(string bugNumber, CancellationTokenSource tokenSource)
        {
            tokenSource.Token.ThrowIfCancellationRequested();
            var arguments = new BugzillaArguments
            {
                UpdateToken = bugNumber,
                CredentialCallback = CredentialCallback
            };

            return await _bugzillaClient.GetBugInformationAsync(arguments, tokenSource).ConfigureAwait(false);
        }

        private void OnCopyToClipboard(string message)
        {
            if (CopyToClipboard != null)
            {
                CopyToClipboard(this, new CopyToClipboardEventArgs(message));
            }
        }

        private Credentials CredentialCallback(string title)
        {
            return CredentialsCallback == null ? null : CredentialsCallback(title);
        }

        private string FigureOutRepositoryPath()
        {
            var projectRoot = Path.GetFullPath(_subversionArguments.LocalProjectRootDirectory);
            var cwd = Path.GetFullPath(_subversionArguments.Cwd);
            var relativePath = cwd.Substring(projectRoot.Length);
            return CombinePathsInUnixFormat(_projectSettings.RepositoryBasePath, relativePath.Replace('\\', '/'));
        }

        private static string CombinePathsInUnixFormat(string basePath, string relativePath)
        {
            var result = new StringBuilder(basePath);
            if (!(basePath.EndsWith("/") || relativePath.StartsWith("/", StringComparison.Ordinal)))
            {
                result.Append('/');
            }
            result.Append(relativePath);
            return result.ToString();
        }
    }
}
