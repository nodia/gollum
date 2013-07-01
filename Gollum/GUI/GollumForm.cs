using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aidon.Tools.Gollum.Bugzilla;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum.GUI
{
    public partial class GollumForm : Form
    {
        private const string ReviewBoardTicketUrl = "[ReviewBoardTicketUrl]";

        private bool _formShown;
        private bool _reviewBoardDone;
        private GollumEngine _engine;
        private BugzillaBug _bug;
        private CancellationTokenSource _getBugCancellation;
        private readonly Regex _bugMatcher;
        private readonly SubversionArguments _subversionArguments;
        private readonly ProjectSettings _projectSettings;

        public GollumForm(ProjectSettings projectSettings, SubversionArguments subversionArguments)
        {
            InitializeComponent();
            _projectSettings = projectSettings;
            _subversionArguments = subversionArguments;
            _bugMatcher = new Regex(@"(?<=(fixed bug #)|(fix for bug #)|(fixed bug )|(fix for bug ))\s?\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Height = 567;
            groupBoxBugzilla.Visible = false;
            groupBoxBugzilla.Enabled = false;
        }

        #region GollumEngine event handlers

        private void EngineOnUpdateStatus(string message)
        {
            UpdateStatus(message);
        }

        private Credentials EngineOnCredentialsCallback(string title)
        {
            var cw = new CredentialsWindow(title);
            var result = cw.ShowDialog(this);
            return result == DialogResult.OK ? cw.GetCredentials() : null;
        }

        #endregion

        #region Form event handlers

        private void GollumFormShown(object sender, EventArgs e)
        {
            try
            {
                _engine = new GollumEngine(_subversionArguments, _projectSettings);
                _engine.UpdateStatus += EngineOnUpdateStatus;
                _engine.CredentialsCallback += EngineOnCredentialsCallback;
                _formShown = true;
                FillFields(_engine.CommitMessage, _engine.CommitRevisionFrom, _engine.CommitRevisionTo, _engine.RepositoryBasePath, _engine.ReviewBoardRepositoryName);
                textBoxReviewBoardSummary.Focus();
                BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private async void ButtonPostReviewClick(object sender, EventArgs ea)
        {
            try
            {
                var success = await PostReviewAsync();
                if (success)
                {
                    Close();
                }
            }
            catch (OperationCanceledException e)
            {
                MessageBox.Show(e.Message, "Operation aborted", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception e)
            {
                if (e.Data["Error details"] != null)
                {
                    string errorDetails = e.Data["Error details"].ToString();
                    MessageBox.Show(e.Message + Environment.NewLine + Environment.NewLine + "Error details: " +
                                    Environment.NewLine + errorDetails, "Review board error!");
                }
                else
                {
                    MessageBox.Show(e.Message, "An error occurred!");
                }
            }
        }

        private void ComboBoxBugStatusSelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxBugStatus.Text == "RESOLVED" || comboBoxBugStatus.Text == "VERIFIED")
            {
                comboBoxBugResolution.Enabled = true;
                if (comboBoxBugResolution.Text.Length == 0)
                {
                    comboBoxBugResolution.Text = "FIXED";
                }
            }
            else
            {
                comboBoxBugResolution.Text = "";
                comboBoxBugResolution.Enabled = false;
            }
        }

        private void TextBoxReviewBoardSummaryTextChanged(object sender, EventArgs e)
        {
            var summary = textBoxReviewBoardSummary.Text.Replace("\r", String.Empty).Replace("\n", " ");
            if (String.IsNullOrWhiteSpace(summary) || String.IsNullOrWhiteSpace(textBoxReviewBoardDescription.Text))
            {
                labelReviewBoardSummaryError.Visible = true;
                UpdateStatus("Input error!");
            }
            else
            {
                if (textBoxReviewBoardSummary.Text != summary)
                {
                    // removed only line changes
                    textBoxReviewBoardSummary.TextChanged -= TextBoxReviewBoardSummaryTextChanged;
                    textBoxReviewBoardSummary.Text = summary;
                    textBoxReviewBoardSummary.TextChanged += TextBoxReviewBoardSummaryTextChanged;
                }
                UpdateFixedBugsField(summary);
                labelReviewBoardSummaryError.Visible = false;
                UpdateStatus("Post review", true);
            }
        }

        private void TextBoxReviewBoardDescriptionTextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxReviewBoardDescription.Text) || String.IsNullOrWhiteSpace(textBoxReviewBoardSummary.Text))
            {
                labelReviewBoardSummaryError.Visible = true;
                UpdateStatus("Input error!");
            }
            else
            {
                labelReviewBoardSummaryError.Visible = false;
                UpdateStatus("Post review", true);
            }
        }

        private async void TextBoxBugsFixedTextChanged(object sender, EventArgs e)
        {
            await SetBugDisplay().ConfigureAwait(false);
        }

        private void GollumFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_getBugCancellation != null)
            {
                _getBugCancellation.Dispose();
            }
        }

        #endregion

        private void SetTicketUrlToClipboard(string url)
        {
            Clipboard.SetText(url);
            if (_bug != null)
            {
                _bug.ReviewBoardTicketLink = url;
            }
        }

        private async Task<bool> PostToReviewBoard()
        {
            StartProgressBar();
            var response = await _engine.PostToReviewBoardAsync(textBoxReviewBoardSummary.Text, textBoxReviewBoardSummary.Text, textBoxBugsFixed.Text);
            if (response != null)
            {
                SetTicketUrlToClipboard(response.ReviewUrl);
                return true;
            }
            return false;
        }

        private void StartProgressBar()
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
        }

        private void StopProgressBar()
        {
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = 0;
        }

        private async Task PostToBugzillaAsync()
        {
            if (_bug == null)
            {
                return;
            }

            string token = _bug.UpdateToken;
            string resolution = comboBoxBugResolution.GetItemText(comboBoxBugResolution.SelectedItem);
            string status = comboBoxBugStatus.GetItemText(comboBoxBugStatus.SelectedItem);

            var bugZillaComment = ConvertBugzillaComment(textBoxBugComment.Text);
            await _engine.PostToBugzillaAsync(textBoxBugNumber.Text,
                                                     resolution,
                                                     status,
                                                     bugZillaComment,
                                                     token).ConfigureAwait(false);
        }

        private string ConvertBugzillaComment(string comment)
        {
            return comment.Replace(ReviewBoardTicketUrl, _bug != null ? _bug.ReviewBoardTicketLink : String.Empty);
        }

        private void UpdateStatus(string message, bool enabled = false)
        {
            if (_formShown)
            {
                if (buttonPostReview.InvokeRequired)
                {
                    buttonPostReview.Invoke((MethodInvoker) delegate
                    {
                        buttonPostReview.Text = message;
                        buttonPostReview.Enabled = enabled;
                    });
                }
                else
                {
                    buttonPostReview.Text = message;
                    buttonPostReview.Enabled = enabled;
                }
            }
        }

        private void FillFields(string commitMessage, int revisionFrom, int revisionTo, string branch, string repository)
        {
            textBoxCommitMessage.Text = commitMessage;

            if (revisionTo - revisionFrom == 1)
            {
                labelFrom.Visible = false;
                labelTo.Visible = false;
                textBoxRevisionTo.Visible = false;
                textBoxRevisionFrom.Text = revisionTo.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                labelFrom.Visible = true;
                labelTo.Visible = true;
                textBoxRevisionTo.Visible = true;

                textBoxRevisionFrom.Text = revisionFrom.ToString(CultureInfo.InvariantCulture);
                textBoxRevisionTo.Text = revisionTo.ToString(CultureInfo.InvariantCulture);
            }

            textBoxSVNRepository.Text = repository;
            textBoxSVNBranch.Text = branch;
            textBoxReviewBoardDescription.Text = commitMessage;
            textBoxReviewBoardSummary.Text = commitMessage;
        }

        private void UpdateFixedBugsField(string commitMessage)
        {
            var bugs = String.Empty;
            var result = _bugMatcher.Match(commitMessage);
            while (result.Success)
            {
                bugs += result;
                result = result.NextMatch();
                if (result.Success)
                {
                    bugs += " ";
                }
            }
            if (bugs != String.Empty && bugs != textBoxBugsFixed.Text)
            {
                textBoxBugsFixed.Text = bugs;
            }
        }

        private void UpdateBugFields()
        {
            if (_bug == null)
            {
                textBoxBugNumber.Text = String.Empty;
                textBoxBugSummary.Text = String.Empty;
                comboBoxBugStatus.Text = String.Empty;
                comboBoxBugResolution.Text = String.Empty;
                textBoxBugComment.Text = String.Empty;
            }
            else
            {
                textBoxBugSummary.ForeColor = Color.Black;
                textBoxBugSummary.Text = _bug.Summary;
                comboBoxBugStatus.Text = _bug.Status;
                comboBoxBugResolution.Text = _bug.Resolution;

                textBoxBugComment.Text = String.Format(@"Fixed in {0} revision {1} of {2}:{5}{3}{5}{5}{4}",
                                                        _engine.ReviewBoardRepositoryName, _engine.CommitRevisionTo, 
                                                        _engine.RepositoryBasePath, ReviewBoardTicketUrl, 
                                                        _engine.CommitMessage, Environment.NewLine);
            }
            ToggleBugzillaVisibility(_bug != null);
            UpdateStatus("Post review", true);
        }

        private async Task SetBugDisplay()
        {
            _bug = null;

            try
            {
                InitializeCancellation();
                await Task.Delay(1500, _getBugCancellation.Token);
                _bug = await GetBugInformationAsync();
            }
            catch (Exception ex)
            {
                _bug = null;
                if (ex is BugzillaAuthenticationException)
                {
                    MessageBox.Show(this, "BugZilla authentication failed.", "Authentication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex is BugzillaException)
                {
                    textBoxBugSummary.Text = ex.Message;
                }
                else if (!(ex is ObjectDisposedException || ex is OperationCanceledException))
                {
                    UpdateStatus("Could not get bug status.", true);
                }
            }
            finally
            {
                UpdateBugFields();
                StopProgressBar();
            }
        }

        private void InitializeCancellation()
        {
            if (_getBugCancellation != null)
            {
                try
                {
                    _getBugCancellation.Cancel();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Failed to cancel and cleanup existing cancellation token: {0}", ex.Message);
                }
                finally
                {
                    _getBugCancellation.Dispose();
                }
            }
            _getBugCancellation = new CancellationTokenSource();
        }

        private async Task<BugzillaBug> GetBugInformationAsync()
        {
            var cancel = _getBugCancellation;
            
            if (textBoxBugsFixed.Text.Length <= 0)
            {
                return null;
            }

            cancel.Token.ThrowIfCancellationRequested();

            ToggleBugzillaVisibility(false);
            StartProgressBar();

            Exception lastException = null;

            var numbers = textBoxBugsFixed.Text.Split(new [] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var bugNumber in numbers)
            {
                cancel.Token.ThrowIfCancellationRequested();

                uint test;
                var nextBug = bugNumber.Trim();
                if (numbers.Length == 0 || !UInt32.TryParse(nextBug, out test))
                {
                    continue;
                }

                UpdateStatus(String.Format("Loading information about bug #{0}...", nextBug));
                cancel.CancelAfter(TimeSpan.FromSeconds(60));
                
                try
                {
                    var bug = await _engine.GetBugInformationAsync(nextBug, cancel.Token);
                    if (bug != null)
                    {
                        textBoxBugNumber.Text = nextBug;
                        return bug;
                    }
                }
                catch (BugzillaException ex)
                {
                    Debug.WriteLine(ex);
                    lastException = ex;
                }
            }

            if (lastException != null)
            {
                throw lastException;
            }

            return null;
        }

        private void ToggleBugzillaVisibility(bool visible)
        {
            groupBoxBugzilla.Visible = visible;
            groupBoxBugzilla.Enabled = visible;
            var size = visible ? new Size(Width, 780) : new Size(Width, 567);
            MaximumSize = size;
            MinimumSize = size;
        }

        private async Task<bool> PostReviewAsync()
        {
            bool success = true;
            try
            {
                StartProgressBar();

                if (!_reviewBoardDone)
                {
                    groupBoxReviewBoard.Enabled = false;
                    groupBoxBugzilla.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonPostReview.Enabled = false;
                    success = await PostToReviewBoard();
                    _reviewBoardDone = success;
                    UpdateStatus(success ? "Review ticket created..." : "Failed to create review ticket!");
                    await Task.Delay(1000);
                }

                if (_reviewBoardDone && _engine.BugzillaEnabled && _bug != null)
                {
                    await PostToBugzillaAsync();
                }
                return success;
            }
            catch (BugzillaAuthenticationException)
            {
                success = false;
                MessageBox.Show("BugZilla authentication failed.", "Authentication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                StopProgressBar();
                if (!success)
                {
                    groupBoxReviewBoard.Enabled = true;
                    if (_engine.BugzillaEnabled && _bug != null)
                    {
                        ToggleBugzillaVisibility(true);
                    }
                    buttonCancel.Enabled = true;
                    buttonPostReview.Enabled = true;
                    UpdateStatus(_reviewBoardDone ? "Update bug" : "Post review", true);
                }
            }
        }
    }
}
