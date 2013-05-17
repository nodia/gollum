using System;
using System.IO;
using System.Text;
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

    /// <summary>
    /// Delegate for sending the review board ticket url back to the user interface as soon as it is discovered.
    /// </summary>
    /// <param name="url">The URL.</param>
    public delegate void ReviewBoardTicketUrlDiscovered(string url);

    public class GollumEngine
    {
        private readonly SubversionArguments _subversionArguments;
        private readonly ProjectSettings _projectSettings;

        private readonly IPatchCreator _patchCreator;
        private readonly IReviewBoardHandler _reviewBoardHandler;
        private readonly IBugzillaHandler _bugzillaClient;

        public event Update UpdateStatus;
        public event ReviewBoardTicketUrlDiscovered TicketDiscovered;
        public event CredentialCallback CredentialsCallback;

        public GollumEngine(SubversionArguments subversionArguments, ProjectSettings projectSettings)
        {
            _projectSettings = projectSettings;
            _subversionArguments = subversionArguments;

            _patchCreator = new SvnPatchCreator();

            _reviewBoardHandler = new ReviewBoardRestClient(Properties.Settings.Default.ReviewBoardUrl);
            _reviewBoardHandler.ReviewIdDiscovered += ReviewBoardHandlerOnReviewIdDiscovered;

            if (!String.IsNullOrEmpty(Properties.Settings.Default.ReviewBoardUrl))
            {
                _bugzillaClient = new BugzillaRestClient(Properties.Settings.Default.BugzillaUrl);
            }
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
        /// <returns></returns>
        public bool PostToReviewBoard(string summary, string description, string bugs)
        {
            try
            {
                PostUpdate("Creating diff...");

                string patch = _patchCreator.CreatePatch(_subversionArguments);

                PostUpdate("Diff created. Creating review ticket...");

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

                _reviewBoardHandler.PostToReviewBoard(arguments);

                return true;
            }
            catch (ReviewBoardAuthenticationException)
            {
                throw;
            }
            catch (ReviewBoardException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Posts an bug update to bugzilla.
        /// </summary>
        /// <param name="bugNumber">The bug number.</param>
        /// <param name="resolution">The resolution.</param>
        /// <param name="status">The status.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="updateToken">The update token.</param>
        /// <returns></returns>
        public bool PostToBugzilla(string bugNumber, string resolution, string status, string comment, string updateToken)
        {
            try
            {
                UpdateStatus("Updating Bugzilla...");
                var arguments = new BugzillaArguments
                {
                    BugId = bugNumber,
                    UpdateToken = updateToken,
                    Comment = comment,
                    Resolution = resolution,
                    Status = status
                };

                _bugzillaClient.PostToBugzilla(arguments);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the bug information.
        /// </summary>
        /// <param name="bugNumber">The bug number.</param>
        /// <returns>Bugzilla bug information.</returns>
        public BugzillaBug GetBugInformation(string bugNumber)
        {
            try
            {
                var arguments = new BugzillaArguments
                {
                    UpdateToken = bugNumber,
                    CredentialCallback = CredentialCallback
                };

                var bug = _bugzillaClient.GetBugInformation(arguments);

                return bug;
            }
            catch (BugzillaException)
            {
                return null;
            }
        }

        private void PostUpdate(string message)
        {
            if (UpdateStatus != null)
            {
                UpdateStatus(message);
            }
        }

        private Credentials CredentialCallback(string title)
        {
            if (CredentialsCallback == null)
            {
                return null;
            }

            return CredentialsCallback(title);
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
            if (!(basePath.EndsWith("/") || relativePath.StartsWith("/")))
            {
                result.Append('/');
            }
            result.Append(relativePath);
            return result.ToString();
        }

        private void ReviewBoardHandlerOnReviewIdDiscovered(object sender, ReviewIdDiscoveredEventArgs reviewIdDiscoveredEventArgs)
        {
            PostUpdate("Review ticket created. Uploading diff...");

            if (TicketDiscovered != null)
            {
                TicketDiscovered(reviewIdDiscoveredEventArgs.ReviewBoardTicketLink);
            }
        }
    }
}
