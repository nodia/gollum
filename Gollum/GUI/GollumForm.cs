using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aidon.Tools.Gollum.Bugzilla;
using Aidon.Tools.Gollum.ReviewBoard;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum.GUI
{
    public partial class GollumForm : Form
    {

        private const int ClientHeightWithoutBugzilla = 450;

        private const int ClientHeightWithBugzilla = 626;

        private const int ClientWidth96Dpi = 650;

        private const int DefaultDpi = 96;

        private const string ReviewBoardTicketUrl = "[ReviewBoardTicketUrl]";

        private bool _formShown;
        private bool _reviewBoardDone;
        private GollumEngine _engine;
        private BugzillaBug _bug;
        private CancellationTokenSource _getBugCancellation;
        private readonly Regex _bugMatcher;
        private readonly SubversionArguments _subversionArguments;
        private readonly ProjectSettings _projectSettings;
        private readonly float _dpiX;
        private readonly float _dpiY;

        public GollumForm(ProjectSettings projectSettings, SubversionArguments subversionArguments)
        {
            InitializeComponent();
            using (var graphics = CreateGraphics())
            {
                _dpiX = graphics.DpiX;
                _dpiY = graphics.DpiY;
            }
            _projectSettings = projectSettings;
            _subversionArguments = subversionArguments;
            _bugMatcher = new Regex(@"(?<=(fixed bug #)|(fix for bug #)|(fixed bug )|(fix for bug ))\s?\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            ToggleBugzillaVisibility(false);
        }

        #region GollumEngine event handlers

        private void OnProgressChanged(object sender, string message)
        {
            UpdateStatus(message);
        }

        private Credentials EngineOnCredentialsCallback(string title)
        {
            using (var cw = new CredentialsWindow(title))
            {
                if (cw.ShowDialog(this) == DialogResult.OK)
                {
                    return cw.GetCredentials();
                }
                throw new TaskCanceledException("User canceled the login operation.");
            }
        }

        #endregion

        #region Form event handlers

        private void GollumFormShown(object sender, EventArgs e)
        {
            try
            {
                _engine = new GollumEngine(_subversionArguments, _projectSettings);
                _engine.ProgressChanged += OnProgressChanged;
                _engine.CopyToClipboard += SetTicketUrlToClipboard;
                _engine.CredentialsCallback += EngineOnCredentialsCallback;
                _formShown = true;
                checkBoxUpdateOnlyBugzilla.Visible = _engine.BugzillaEnabled;
                FillFields(_engine.CommitMessage, _engine.CommitRevisionFrom, _engine.CommitRevisionTo, _engine.RepositoryBasePath, _engine.ReviewBoardRepositoryName);
                textBoxReviewBoardSummary.Focus();
                BringToFront();

                var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gollum.log");
                var filestream = new FileStream(logPath, FileMode.Create);
                var streamwriter = new StreamWriter(filestream) { AutoFlush = true };
                Console.SetError(streamwriter);
                Console.SetOut(streamwriter);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
                Console.WriteLine(e);
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
                UpdateStatus(GetReviewButtonText(), true);
                if (textBoxReviewBoardSummary.Text != summary)
                {
                    // removed only line changes
                    textBoxReviewBoardSummary.TextChanged -= TextBoxReviewBoardSummaryTextChanged;
                    textBoxReviewBoardSummary.Text = summary;
                    textBoxReviewBoardSummary.TextChanged += TextBoxReviewBoardSummaryTextChanged;
                }
                UpdateFixedBugsField(summary);
                labelReviewBoardSummaryError.Visible = false;
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
                UpdateStatus(GetReviewButtonText(), true);
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

        private void CheckBoxUpdateOnlyBugzillaCheckedChanged(object sender, EventArgs e)
        {
            if (buttonPostReview.Enabled)
            {
                UpdateStatus(GetReviewButtonText(), true);
            }
        }

        #endregion

        private void SetTicketUrlToClipboard(object sender, CopyToClipboardEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => SetTicketUrlToClipboard(e.Message)));
            }
            else
            {
                SetTicketUrlToClipboard(e.Message);
            }
        }

        private void SetTicketUrlToClipboard(string url)
        {
            Clipboard.SetText(url);
            if (_bug != null)
            {
                _bug.ReviewBoardTicketLink = url;
            }
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

        private async Task PostToReviewBoardAsync(string summary, string description, string bugs)
        {
            while (true)
            {
                try
                {
                    await _engine.PostToReviewBoardAsync(summary, description, bugs).ConfigureAwait(false);
                    break;
                }
                catch (ReviewBoardAuthenticationException ex)
                {
                    Console.WriteLine("Reviewboard authentication failed: " + ex);
                }
            }
        }

        private async Task PostToBugzillaAsync(string token, string resolution, string status, string bugNumber, string bugzillaComment)
        {
            if (_bug == null)
            {
                return;
            }

            while (true)
            {
                try
                {
                    await _engine.PostToBugzillaAsync(bugNumber,
                                                      resolution,
                                                      status,
                                                      bugzillaComment,
                                                      token).ConfigureAwait(false);
                    break;
                }
                catch (BugzillaAuthenticationException ex)
                {
                    Console.WriteLine("Bugzilla authentication failed: " + ex);
                }
            }
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
            UpdateStatus(GetReviewButtonText(), true);
        }

        private async Task SetBugDisplay()
        {
            _bug = null;
            while (true)
            {
                try
                {
                    StartProgressBar();
                    InitializeCancellation();
                    UpdateStatus(GetReviewButtonText());

                    await Task.Delay(1500, _getBugCancellation.Token);
                    _bug = await GetBugInformationAsync();

                    UpdateBugFields();
                    return;
                }
                catch (Exception ex)
                {
                    _bug = null;

                    checkBoxUpdateOnlyBugzilla.Checked = false;
                    ToggleBugzillaVisibility(false);

                    if (ex is TaskCanceledException)
                    {
                        UpdateStatus(GetReviewButtonText(), true);
                        return;
                    }

                    Console.WriteLine(ex);

                    if (!(ex is BugzillaAuthenticationException))
                    {
                        MessageBox.Show(this, "Could not get bug status. See log for more information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatus(GetReviewButtonText(), true);
                        return;
                    }
                    
                    UpdateStatus("Authentication failed");
                    
                }
                finally
                {
                    StopProgressBar();
                }

                await Task.Delay(1500);
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
                    Console.WriteLine("Failed to cancel and cleanup existing cancellation token: {0}", ex.Message);
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
                EnableUpdateOnlyBugzilla(false);
                return null;
            }

            cancel.Token.ThrowIfCancellationRequested();

            ToggleBugzillaVisibility(false);

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
                
                try
                {
                    var bug = await _engine.GetBugInformationAsync(nextBug, cancel);
                    if (bug != null)
                    {
                        EnableUpdateOnlyBugzilla(true);
                        textBoxBugNumber.Text = nextBug;
                        return bug;
                    }
                }
                catch (BugzillaException ex)
                {
                    Console.WriteLine(ex);
                    lastException = ex;
                }
            }

            EnableUpdateOnlyBugzilla(false);

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
            var width = DpiScale(ClientWidth96Dpi, _dpiX);
            var size = visible ? new Size(width, DpiScale(ClientHeightWithBugzilla, _dpiY)) : new Size(width, DpiScale(ClientHeightWithoutBugzilla, _dpiY));
            MaximumSize = size;
            MinimumSize = size;
            ClientSize = size;
        }

        private static int DpiScale(int value, float dpi)
        {
            return (int) (value * (dpi / DefaultDpi));
        }

        private void EnableUpdateOnlyBugzilla(bool update)
        {
            checkBoxUpdateOnlyBugzilla.Enabled = update;
            if (!update)
            {
                checkBoxUpdateOnlyBugzilla.Checked = false;
            }
        }

        private async Task<bool> PostReviewAsync()
        {
            bool success = true;
            bool updateOnlyBugzilla = checkBoxUpdateOnlyBugzilla.Checked;

            groupBoxReviewBoard.Enabled = false;
            groupBoxBugzilla.Enabled = false;
            buttonCancel.Enabled = false;
            buttonPostReview.Enabled = false;

            try
            {
                StartProgressBar();

                if (!_reviewBoardDone && !updateOnlyBugzilla)
                {
                    await PostToReviewBoardAsync(textBoxReviewBoardSummary.Text, textBoxReviewBoardDescription.Text, textBoxBugsFixed.Text);

                    _reviewBoardDone = true;

                    await Task.Delay(1000);
                }

                if (_engine.BugzillaEnabled && _bug != null && (_reviewBoardDone || updateOnlyBugzilla))
                {
                    string bugZillaComment = ConvertBugzillaComment(textBoxBugComment.Text);

                    await PostToBugzillaAsync(_bug.UpdateToken, comboBoxBugResolution.GetItemText(comboBoxBugResolution.SelectedItem), 
                                              comboBoxBugStatus.GetItemText(comboBoxBugStatus.SelectedItem), 
                                              textBoxBugNumber.Text, bugZillaComment);
                }
                return true;
            }
            catch (TaskCanceledException)
            {
                success = false;
                return false;
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine(ex);
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
                    UpdateStatus(GetReviewButtonText(), true);
                }
            }
        }

        private string GetReviewButtonText()
        {
            return _engine.BugzillaEnabled && (_reviewBoardDone || checkBoxUpdateOnlyBugzilla.Checked)
                        ? "Update bug"
                        : "Post review";
        }
    }
}
