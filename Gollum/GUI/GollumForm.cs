using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Aidon.Tools.Gollum.Bugzilla;
using Timer = System.Threading.Timer;

namespace Aidon.Tools.Gollum.GUI
{
    public partial class GollumForm : Form
    {
        private bool _formShown;
        private bool _reviewBoardDone;
        private Timer _bugInputTimer;

        private readonly GollumEngine _engine;

        private BugzillaBug _bug;

        public GollumForm(GollumEngine engine)
        {
            InitializeComponent();

            Height = 567;
            groupBoxBugzilla.Visible = false;
            groupBoxBugzilla.Enabled = false;

            _engine = engine;
            _engine.UpdateStatus += EngineOnUpdateStatus;
            _engine.TicketDiscovered += EngineOnTicketDiscovered;
            _engine.CredentialsCallback += EngineOnCredentialsCallback;
        }

        #region GollumEngine event handlers

        private void EngineOnTicketDiscovered(string url)
        {
            Invoke(new Action(() => Clipboard.SetText(url)));

            if (_bug != null)
            {
                _bug.ReviewBoardTicketLink = url;
            }
        }

        private void EngineOnUpdateStatus(string message)
        {
            UpdateStatus(message);
        }

        private Credentials EngineOnCredentialsCallback(string title)
        {
            Credentials credentials = null;
            Invoke(new Action(() =>
            {
                var cw = new CredentialsWindow(title);
                cw.ShowDialog(this);
                credentials = cw.GetCredentials();
            }));

            return credentials;
        }

        #endregion

        #region Form event handlers
        private void GollumFormShown(object sender, EventArgs e)
        {
            _formShown = true;

            FillFields(_engine.CommitMessage, _engine.CommitRevisionFrom, _engine.CommitRevisionTo, _engine.RepositoryBasePath, _engine.ReviewBoardRepositoryName);
            textBoxReviewBoardSummary.Focus();
            BringToFront();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonPostReviewClick(object sender, EventArgs ea)
        {
            if (!_reviewBoardDone)
            {
                groupBoxReviewBoard.Enabled = false;
                groupBoxBugzilla.Enabled = false;
                buttonCancel.Enabled = false;
                buttonPostReview.Enabled = false;

                new Thread(PostToReviewBoard).Start();
            }
            else if (_engine.BugzillaEnabled && _bug != null)
            {
                new Thread(PostToBugzilla).Start();
            }
        }
        #endregion

        private void PostToReviewBoard()
        {
            try
            {
                StartProgressBar();

                if (_engine.PostToReviewBoard(textBoxReviewBoardSummary.Text, textBoxReviewBoardSummary.Text, textBoxBugsFixed.Text))
                {
                    _reviewBoardDone = true;

                    if (_engine.BugzillaEnabled && _bug != null)
                    {
                        PostToBugzilla();
                    }
                    else
                    {
                        Invoke(new Action(Close));
                    }
                }
                else
                {
                    _reviewBoardDone = false;

                    Invoke(new Action(() =>
                        {
                            StopProgressBar();
                            groupBoxReviewBoard.Enabled = true;
                            groupBoxBugzilla.Enabled = true;
                            buttonCancel.Enabled = true;
                            UpdateStatus("Post review", true);
                        }));
                }
            }
            catch (Exception e)
            {
                StopProgressBar();

                if (e.Data["Error details"] != null)
                {
                    string errorDetails = e.Data["Error details"].ToString();
                    MessageBox.Show(
                        e.Message + Environment.NewLine + Environment.NewLine + "Error details: " +
                        Environment.NewLine + errorDetails, "Review board error!");
                }
                else
                {
                    MessageBox.Show(e.Message, "An error occurred!");
                }

                Invoke(new Action(() =>
                {
                    StopProgressBar();
                    buttonCancel.Enabled = true;
                }));

                UpdateStatus("An error occurred while posting review!");
            }
        }

        private void StartProgressBar()
        {
            Invoke(new Action(() =>
                {
                    progressBar.Style = ProgressBarStyle.Marquee;
                    progressBar.MarqueeAnimationSpeed = 30;
                }));
        }

        private void StopProgressBar()
        {
            Invoke(new Action(() =>
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.MarqueeAnimationSpeed = 0;
            }));
        }

        private void PostToBugzilla()
        {
            try
            {
                if (_bug == null)
                {
                    Invoke(new Action(Close));
                    return;
                }

                string resolution = null;
                string status = null;
                Invoke(
                    new Action(
                        () =>
                            {
                                resolution = comboBoxBugResolution.GetItemText(comboBoxBugResolution.SelectedItem);
                                status = comboBoxBugStatus.GetItemText(comboBoxBugStatus.SelectedItem);
                            }));

                if (_engine.PostToBugzilla(textBoxBugNumber.Text,
                                           resolution,
                                           status,
                                           ConvertBugzillaComment(textBoxBugComment.Text),
                                           _bug.UpdateToken))
                {
                    Invoke(new Action(Close));
                }
                else
                {
                    Invoke(new Action(() =>
                        {
                            StopProgressBar();
                            groupBoxReviewBoard.Enabled = true;
                            buttonCancel.Enabled = true;
                        }));

                    UpdateStatus("Update bug", true);
                }
            }
            catch (Exception e)
            {
                StopProgressBar();

                if (e.Data["Error details"] != null)
                {
                    string errorDetails = e.Data["Error details"].ToString();
                    MessageBox.Show(
                        e.Message + Environment.NewLine + Environment.NewLine + "Error details: " +
                        Environment.NewLine + errorDetails, "Bugzilla error!");
                }
                else
                {
                    MessageBox.Show(e.Message, "An error occurred!");
                }

                Invoke(new Action(() =>
                {
                    StopProgressBar();
                    buttonCancel.Enabled = true;
                }));

                UpdateStatus("An error occurred while updating bug!");
            }
        }

        private void GetBugInformation(string bugNumber)
        {
            try
            {
                _bug = _engine.GetBugInformation(bugNumber);
            }
            catch (BugzillaException exception)
            {
                _bug = null;
                UpdateBugFields();
                Invoke(new Action(() => textBoxBugSummary.Text = exception.Message));
                StopProgressBar();
                return;
            }
            catch (Exception)
            {
                _bug = null;
            }

            UpdateBugFields();
            StopProgressBar();
        }

        private string ConvertBugzillaComment(string comment)
        {
            return comment.Replace("[ReviewBoardTicketUrl]", _bug != null ? _bug.ReviewBoardTicketLink : "");
        }

        private void UpdateStatus(string message, bool enabled = false)
        {
            if (_formShown)
            {
                Invoke(new Action(() =>
                {
                    buttonPostReview.Text = message;
                    buttonPostReview.Enabled = enabled;
                }));
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

            if (commitMessage.ToLower().Contains("bug") && commitMessage.ToLower().Contains("fix"))
            {
                var resultString = Regex.Match(commitMessage, @"\d+").Value;
                textBoxBugsFixed.Text = resultString;
                textBoxBugNumber.Text = resultString;
            }
        }

        private void UpdateBugFields()
        {
            if (_bug == null)
            {
                Invoke(new Action(() =>
                    {
                        textBoxBugNumber.Text = String.Empty;
                        textBoxBugSummary.Text = String.Empty;
                        comboBoxBugStatus.Text = String.Empty;
                        comboBoxBugResolution.Text = String.Empty;
                        textBoxBugComment.Text = String.Empty;
                    }));
            }
            else
            {
                Invoke(new Action(() =>
                    {
                        textBoxBugSummary.ForeColor = System.Drawing.Color.Black;

                        textBoxBugSummary.Text = _bug.Summary;
                        comboBoxBugStatus.Text = _bug.Status;
                        comboBoxBugResolution.Text = _bug.Resolution;

                        textBoxBugComment.Text = "Fixed in " + _engine.ReviewBoardRepositoryName + " revision " +
                                                       _engine.CommitRevisionTo + " of " +
                                                       _engine.RepositoryBasePath + Environment.NewLine +
                                                       "[ReviewBoardTicketUrl]" + Environment.NewLine +
                                                       Environment.NewLine +
                                                       _engine.CommitMessage;
                    }));
            }

            UpdateStatus("Post review", true);
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
            var summary = textBoxReviewBoardSummary.Text;
            if (summary.Contains(Environment.NewLine) || summary.Contains("\r") || summary.Contains("\n"))
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

        private void TextBoxBugsFixedTextChanged(object sender, EventArgs e)
        {
            UpdateStatus("Loading bug information...");
            StartProgressBar();

            if (_bugInputTimer == null)
            {
                _bugInputTimer = new Timer(BugInputDetected, null, 1500, Timeout.Infinite);
            }
            else
            {
                _bugInputTimer.Change(1500, Timeout.Infinite);
            }
        }

        private void BugInputDetected(object state)
        {
            Invoke(new Action(SetBugDisplay));
        }

        private void SetBugDisplay()
        {
            try
            {
                if (textBoxBugsFixed.Text.Length > 0)
                {
                    groupBoxBugzilla.Visible = true;
                    groupBoxBugzilla.Enabled = true;
                    Height = 780;

                    string number = Regex.Match(textBoxBugsFixed.Text, @"\d+").Value;
                    textBoxBugNumber.Text = number;
                    new Thread(() => GetBugInformation(number)).Start();
                }
                else
                {
                    groupBoxBugzilla.Visible = false;
                    groupBoxBugzilla.Enabled = false;
                    Height = 567;

                    UpdateBugFields();
                    StopProgressBar();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
