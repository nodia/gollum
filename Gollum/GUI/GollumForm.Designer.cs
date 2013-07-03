using System.Windows.Forms;

namespace Aidon.Tools.Gollum.GUI
{
    partial class GollumForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GollumForm));
            this.labelRevision = new System.Windows.Forms.Label();
            this.labelReviewBoardDescription = new System.Windows.Forms.Label();
            this.textBoxReviewBoardDescription = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelReviewBoardSummaryError = new System.Windows.Forms.Label();
            this.textBoxReviewBoardSummary = new System.Windows.Forms.TextBox();
            this.textBoxBugsFixed = new System.Windows.Forms.TextBox();
            this.labelBugsFixed = new System.Windows.Forms.Label();
            this.labelReviewBoardSummary = new System.Windows.Forms.Label();
            this.labelCommitMessage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxPostReview = new System.Windows.Forms.GroupBox();
            this.buttonPostReview = new System.Windows.Forms.Button();
            this.groupBoxSVN = new System.Windows.Forms.GroupBox();
            this.textBoxSVNBranch = new System.Windows.Forms.TextBox();
            this.labelBranch = new System.Windows.Forms.Label();
            this.textBoxSVNRepository = new System.Windows.Forms.TextBox();
            this.labelOf = new System.Windows.Forms.Label();
            this.textBoxCommitMessage = new System.Windows.Forms.TextBox();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxRevisionTo = new System.Windows.Forms.TextBox();
            this.textBoxRevisionFrom = new System.Windows.Forms.TextBox();
            this.groupBoxReviewBoard = new System.Windows.Forms.GroupBox();
            this.checkBoxUpdateOnlyBugzilla = new System.Windows.Forms.CheckBox();
            this.groupBoxBugzilla = new System.Windows.Forms.GroupBox();
            this.labelDash = new System.Windows.Forms.Label();
            this.labelBug = new System.Windows.Forms.Label();
            this.textBoxBugNumber = new System.Windows.Forms.TextBox();
            this.comboBoxBugResolution = new System.Windows.Forms.ComboBox();
            this.comboBoxBugStatus = new System.Windows.Forms.ComboBox();
            this.labelBugStatus = new System.Windows.Forms.Label();
            this.labelBugComment = new System.Windows.Forms.Label();
            this.textBoxBugComment = new System.Windows.Forms.TextBox();
            this.textBoxBugSummary = new System.Windows.Forms.TextBox();
            this.groupBoxPostReview.SuspendLayout();
            this.groupBoxSVN.SuspendLayout();
            this.groupBoxReviewBoard.SuspendLayout();
            this.groupBoxBugzilla.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRevision
            // 
            this.labelRevision.Location = new System.Drawing.Point(16, 20);
            this.labelRevision.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRevision.Name = "labelRevision";
            this.labelRevision.Size = new System.Drawing.Size(68, 16);
            this.labelRevision.TabIndex = 0;
            this.labelRevision.Text = "Revision";
            // 
            // labelReviewBoardDescription
            // 
            this.labelReviewBoardDescription.Location = new System.Drawing.Point(16, 109);
            this.labelReviewBoardDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReviewBoardDescription.Name = "labelReviewBoardDescription";
            this.labelReviewBoardDescription.Size = new System.Drawing.Size(379, 16);
            this.labelReviewBoardDescription.TabIndex = 2;
            this.labelReviewBoardDescription.Text = "Review Board description";
            // 
            // textBoxReviewBoardDescription
            // 
            this.textBoxReviewBoardDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReviewBoardDescription.Location = new System.Drawing.Point(16, 129);
            this.textBoxReviewBoardDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxReviewBoardDescription.Multiline = true;
            this.textBoxReviewBoardDescription.Name = "textBoxReviewBoardDescription";
            this.textBoxReviewBoardDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReviewBoardDescription.Size = new System.Drawing.Size(819, 76);
            this.textBoxReviewBoardDescription.TabIndex = 2;
            this.textBoxReviewBoardDescription.TextChanged += new System.EventHandler(this.TextBoxReviewBoardDescriptionTextChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonCancel.Location = new System.Drawing.Point(412, 19);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(429, 59);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "No review";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // labelReviewBoardSummaryError
            // 
            this.labelReviewBoardSummaryError.ForeColor = System.Drawing.Color.Red;
            this.labelReviewBoardSummaryError.Location = new System.Drawing.Point(189, 26);
            this.labelReviewBoardSummaryError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReviewBoardSummaryError.Name = "labelReviewBoardSummaryError";
            this.labelReviewBoardSummaryError.Size = new System.Drawing.Size(456, 16);
            this.labelReviewBoardSummaryError.TabIndex = 6;
            this.labelReviewBoardSummaryError.Text = "Review board summary and description are required!";
            this.labelReviewBoardSummaryError.Visible = false;
            // 
            // textBoxReviewBoardSummary
            // 
            this.textBoxReviewBoardSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReviewBoardSummary.Location = new System.Drawing.Point(16, 46);
            this.textBoxReviewBoardSummary.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxReviewBoardSummary.Multiline = true;
            this.textBoxReviewBoardSummary.Name = "textBoxReviewBoardSummary";
            this.textBoxReviewBoardSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReviewBoardSummary.Size = new System.Drawing.Size(819, 59);
            this.textBoxReviewBoardSummary.TabIndex = 5;
            this.textBoxReviewBoardSummary.TextChanged += new System.EventHandler(this.TextBoxReviewBoardSummaryTextChanged);
            // 
            // textBoxBugsFixed
            // 
            this.textBoxBugsFixed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugsFixed.Location = new System.Drawing.Point(93, 213);
            this.textBoxBugsFixed.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBugsFixed.Name = "textBoxBugsFixed";
            this.textBoxBugsFixed.Size = new System.Drawing.Size(552, 22);
            this.textBoxBugsFixed.TabIndex = 4;
            this.textBoxBugsFixed.TextChanged += new System.EventHandler(this.TextBoxBugsFixedTextChanged);
            // 
            // labelBugsFixed
            // 
            this.labelBugsFixed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBugsFixed.Location = new System.Drawing.Point(13, 216);
            this.labelBugsFixed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBugsFixed.Name = "labelBugsFixed";
            this.labelBugsFixed.Size = new System.Drawing.Size(80, 20);
            this.labelBugsFixed.TabIndex = 3;
            this.labelBugsFixed.Text = "Bugs fixed";
            // 
            // labelReviewBoardSummary
            // 
            this.labelReviewBoardSummary.Location = new System.Drawing.Point(16, 26);
            this.labelReviewBoardSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelReviewBoardSummary.Name = "labelReviewBoardSummary";
            this.labelReviewBoardSummary.Size = new System.Drawing.Size(168, 25);
            this.labelReviewBoardSummary.TabIndex = 2;
            this.labelReviewBoardSummary.Text = "Review Board summary";
            // 
            // labelCommitMessage
            // 
            this.labelCommitMessage.Location = new System.Drawing.Point(16, 52);
            this.labelCommitMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCommitMessage.Name = "labelCommitMessage";
            this.labelCommitMessage.Size = new System.Drawing.Size(143, 23);
            this.labelCommitMessage.TabIndex = 2;
            this.labelCommitMessage.Text = "Commit message";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(4, 78);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.MarqueeAnimationSpeed = 30;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(837, 28);
            this.progressBar.TabIndex = 10;
            // 
            // groupBoxPostReview
            // 
            this.groupBoxPostReview.Controls.Add(this.buttonPostReview);
            this.groupBoxPostReview.Controls.Add(this.buttonCancel);
            this.groupBoxPostReview.Controls.Add(this.progressBar);
            this.groupBoxPostReview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxPostReview.Location = new System.Drawing.Point(0, 613);
            this.groupBoxPostReview.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxPostReview.MinimumSize = new System.Drawing.Size(845, 103);
            this.groupBoxPostReview.Name = "groupBoxPostReview";
            this.groupBoxPostReview.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxPostReview.Size = new System.Drawing.Size(845, 110);
            this.groupBoxPostReview.TabIndex = 11;
            this.groupBoxPostReview.TabStop = false;
            this.groupBoxPostReview.Text = "Post review?";
            // 
            // buttonPostReview
            // 
            this.buttonPostReview.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonPostReview.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonPostReview.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonPostReview.Location = new System.Drawing.Point(4, 19);
            this.buttonPostReview.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPostReview.Name = "buttonPostReview";
            this.buttonPostReview.Size = new System.Drawing.Size(408, 59);
            this.buttonPostReview.TabIndex = 11;
            this.buttonPostReview.Text = "Post review";
            this.buttonPostReview.Click += new System.EventHandler(this.ButtonPostReviewClick);
            // 
            // groupBoxSVN
            // 
            this.groupBoxSVN.Controls.Add(this.textBoxSVNBranch);
            this.groupBoxSVN.Controls.Add(this.labelBranch);
            this.groupBoxSVN.Controls.Add(this.textBoxSVNRepository);
            this.groupBoxSVN.Controls.Add(this.labelOf);
            this.groupBoxSVN.Controls.Add(this.textBoxCommitMessage);
            this.groupBoxSVN.Controls.Add(this.labelTo);
            this.groupBoxSVN.Controls.Add(this.labelFrom);
            this.groupBoxSVN.Controls.Add(this.textBoxRevisionTo);
            this.groupBoxSVN.Controls.Add(this.textBoxRevisionFrom);
            this.groupBoxSVN.Controls.Add(this.labelCommitMessage);
            this.groupBoxSVN.Controls.Add(this.labelRevision);
            this.groupBoxSVN.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSVN.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSVN.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxSVN.MaximumSize = new System.Drawing.Size(845, 148);
            this.groupBoxSVN.MinimumSize = new System.Drawing.Size(845, 148);
            this.groupBoxSVN.Name = "groupBoxSVN";
            this.groupBoxSVN.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSVN.Size = new System.Drawing.Size(845, 148);
            this.groupBoxSVN.TabIndex = 12;
            this.groupBoxSVN.TabStop = false;
            this.groupBoxSVN.Text = "SVN";
            // 
            // textBoxSVNBranch
            // 
            this.textBoxSVNBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSVNBranch.Location = new System.Drawing.Point(648, 15);
            this.textBoxSVNBranch.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSVNBranch.Name = "textBoxSVNBranch";
            this.textBoxSVNBranch.ReadOnly = true;
            this.textBoxSVNBranch.Size = new System.Drawing.Size(189, 22);
            this.textBoxSVNBranch.TabIndex = 12;
            // 
            // labelBranch
            // 
            this.labelBranch.Location = new System.Drawing.Point(587, 20);
            this.labelBranch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(59, 16);
            this.labelBranch.TabIndex = 11;
            this.labelBranch.Text = "branch";
            // 
            // textBoxSVNRepository
            // 
            this.textBoxSVNRepository.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSVNRepository.Location = new System.Drawing.Point(385, 16);
            this.textBoxSVNRepository.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSVNRepository.Name = "textBoxSVNRepository";
            this.textBoxSVNRepository.ReadOnly = true;
            this.textBoxSVNRepository.Size = new System.Drawing.Size(192, 22);
            this.textBoxSVNRepository.TabIndex = 10;
            // 
            // labelOf
            // 
            this.labelOf.Location = new System.Drawing.Point(347, 20);
            this.labelOf.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOf.Name = "labelOf";
            this.labelOf.Size = new System.Drawing.Size(31, 16);
            this.labelOf.TabIndex = 9;
            this.labelOf.Text = "of";
            // 
            // textBoxCommitMessage
            // 
            this.textBoxCommitMessage.Location = new System.Drawing.Point(175, 48);
            this.textBoxCommitMessage.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCommitMessage.Multiline = true;
            this.textBoxCommitMessage.Name = "textBoxCommitMessage";
            this.textBoxCommitMessage.ReadOnly = true;
            this.textBoxCommitMessage.Size = new System.Drawing.Size(661, 85);
            this.textBoxCommitMessage.TabIndex = 8;
            // 
            // labelTo
            // 
            this.labelTo.Location = new System.Drawing.Point(248, 20);
            this.labelTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(25, 16);
            this.labelTo.TabIndex = 7;
            this.labelTo.Text = "to";
            this.labelTo.Visible = false;
            // 
            // labelFrom
            // 
            this.labelFrom.Location = new System.Drawing.Point(131, 20);
            this.labelFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(37, 16);
            this.labelFrom.TabIndex = 6;
            this.labelFrom.Text = "from";
            this.labelFrom.Visible = false;
            // 
            // textBoxRevisionTo
            // 
            this.textBoxRevisionTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRevisionTo.Location = new System.Drawing.Point(272, 16);
            this.textBoxRevisionTo.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRevisionTo.Name = "textBoxRevisionTo";
            this.textBoxRevisionTo.ReadOnly = true;
            this.textBoxRevisionTo.Size = new System.Drawing.Size(65, 22);
            this.textBoxRevisionTo.TabIndex = 5;
            this.textBoxRevisionTo.Visible = false;
            // 
            // textBoxRevisionFrom
            // 
            this.textBoxRevisionFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRevisionFrom.Location = new System.Drawing.Point(176, 16);
            this.textBoxRevisionFrom.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRevisionFrom.Name = "textBoxRevisionFrom";
            this.textBoxRevisionFrom.ReadOnly = true;
            this.textBoxRevisionFrom.Size = new System.Drawing.Size(65, 22);
            this.textBoxRevisionFrom.TabIndex = 4;
            // 
            // groupBoxReviewBoard
            // 
            this.groupBoxReviewBoard.Controls.Add(this.checkBoxUpdateOnlyBugzilla);
            this.groupBoxReviewBoard.Controls.Add(this.textBoxBugsFixed);
            this.groupBoxReviewBoard.Controls.Add(this.textBoxReviewBoardSummary);
            this.groupBoxReviewBoard.Controls.Add(this.labelBugsFixed);
            this.groupBoxReviewBoard.Controls.Add(this.labelReviewBoardSummaryError);
            this.groupBoxReviewBoard.Controls.Add(this.textBoxReviewBoardDescription);
            this.groupBoxReviewBoard.Controls.Add(this.labelReviewBoardDescription);
            this.groupBoxReviewBoard.Controls.Add(this.labelReviewBoardSummary);
            this.groupBoxReviewBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxReviewBoard.Location = new System.Drawing.Point(0, 148);
            this.groupBoxReviewBoard.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxReviewBoard.MaximumSize = new System.Drawing.Size(843, 240);
            this.groupBoxReviewBoard.MinimumSize = new System.Drawing.Size(843, 240);
            this.groupBoxReviewBoard.Name = "groupBoxReviewBoard";
            this.groupBoxReviewBoard.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxReviewBoard.Size = new System.Drawing.Size(843, 240);
            this.groupBoxReviewBoard.TabIndex = 9;
            this.groupBoxReviewBoard.TabStop = false;
            this.groupBoxReviewBoard.Text = "Review Board";
            // 
            // checkBoxUpdateOnlyBugzilla
            // 
            this.checkBoxUpdateOnlyBugzilla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUpdateOnlyBugzilla.AutoSize = true;
            this.checkBoxUpdateOnlyBugzilla.Enabled = false;
            this.checkBoxUpdateOnlyBugzilla.Location = new System.Drawing.Point(652, 212);
            this.checkBoxUpdateOnlyBugzilla.Name = "checkBoxUpdateOnlyBugzilla";
            this.checkBoxUpdateOnlyBugzilla.Size = new System.Drawing.Size(159, 21);
            this.checkBoxUpdateOnlyBugzilla.TabIndex = 7;
            this.checkBoxUpdateOnlyBugzilla.Text = "Update only Bugzilla";
            this.checkBoxUpdateOnlyBugzilla.UseVisualStyleBackColor = true;
            this.checkBoxUpdateOnlyBugzilla.CheckedChanged += new System.EventHandler(this.CheckBoxUpdateOnlyBugzillaCheckedChanged);
            // 
            // groupBoxBugzilla
            // 
            this.groupBoxBugzilla.Controls.Add(this.labelDash);
            this.groupBoxBugzilla.Controls.Add(this.labelBug);
            this.groupBoxBugzilla.Controls.Add(this.textBoxBugNumber);
            this.groupBoxBugzilla.Controls.Add(this.comboBoxBugResolution);
            this.groupBoxBugzilla.Controls.Add(this.comboBoxBugStatus);
            this.groupBoxBugzilla.Controls.Add(this.labelBugStatus);
            this.groupBoxBugzilla.Controls.Add(this.labelBugComment);
            this.groupBoxBugzilla.Controls.Add(this.textBoxBugComment);
            this.groupBoxBugzilla.Controls.Add(this.textBoxBugSummary);
            this.groupBoxBugzilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBugzilla.Location = new System.Drawing.Point(0, 388);
            this.groupBoxBugzilla.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxBugzilla.Name = "groupBoxBugzilla";
            this.groupBoxBugzilla.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxBugzilla.Size = new System.Drawing.Size(843, 225);
            this.groupBoxBugzilla.TabIndex = 10;
            this.groupBoxBugzilla.TabStop = false;
            this.groupBoxBugzilla.Text = "Bugzilla";
            // 
            // labelDash
            // 
            this.labelDash.Location = new System.Drawing.Point(161, 33);
            this.labelDash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDash.Name = "labelDash";
            this.labelDash.Size = new System.Drawing.Size(13, 16);
            this.labelDash.TabIndex = 8;
            this.labelDash.Text = "-";
            // 
            // labelBug
            // 
            this.labelBug.Location = new System.Drawing.Point(16, 33);
            this.labelBug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBug.Name = "labelBug";
            this.labelBug.Size = new System.Drawing.Size(37, 23);
            this.labelBug.TabIndex = 7;
            this.labelBug.Text = "Bug";
            // 
            // textBoxBugNumber
            // 
            this.textBoxBugNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugNumber.Location = new System.Drawing.Point(55, 30);
            this.textBoxBugNumber.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBugNumber.Name = "textBoxBugNumber";
            this.textBoxBugNumber.ReadOnly = true;
            this.textBoxBugNumber.Size = new System.Drawing.Size(102, 22);
            this.textBoxBugNumber.TabIndex = 6;
            // 
            // comboBoxBugResolution
            // 
            this.comboBoxBugResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBugResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBugResolution.Enabled = false;
            this.comboBoxBugResolution.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxBugResolution.Items.AddRange(new object[] {
            "FIXED",
            "INVALID",
            "WONTFIX",
            "DUPLICATE",
            "WORKSFORME"});
            this.comboBoxBugResolution.Location = new System.Drawing.Point(510, 193);
            this.comboBoxBugResolution.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBugResolution.Name = "comboBoxBugResolution";
            this.comboBoxBugResolution.Size = new System.Drawing.Size(325, 24);
            this.comboBoxBugResolution.TabIndex = 5;
            // 
            // comboBoxBugStatus
            // 
            this.comboBoxBugStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBugStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBugStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxBugStatus.Items.AddRange(new object[] {
            "UNCONFIRMED",
            "CONFIRMED",
            "IN_PROGRESS",
            "RESOLVED",
            "VERIFIED"});
            this.comboBoxBugStatus.Location = new System.Drawing.Point(174, 193);
            this.comboBoxBugStatus.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBugStatus.Name = "comboBoxBugStatus";
            this.comboBoxBugStatus.Size = new System.Drawing.Size(327, 24);
            this.comboBoxBugStatus.TabIndex = 4;
            this.comboBoxBugStatus.SelectedValueChanged += new System.EventHandler(this.ComboBoxBugStatusSelectedValueChanged);
            // 
            // labelBugStatus
            // 
            this.labelBugStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBugStatus.Location = new System.Drawing.Point(16, 197);
            this.labelBugStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBugStatus.Name = "labelBugStatus";
            this.labelBugStatus.Size = new System.Drawing.Size(143, 20);
            this.labelBugStatus.TabIndex = 3;
            this.labelBugStatus.Text = "Bug status";
            // 
            // labelBugComment
            // 
            this.labelBugComment.Location = new System.Drawing.Point(16, 57);
            this.labelBugComment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBugComment.Name = "labelBugComment";
            this.labelBugComment.Size = new System.Drawing.Size(149, 20);
            this.labelBugComment.TabIndex = 2;
            this.labelBugComment.Text = "Bugzilla comment";
            // 
            // textBoxBugComment
            // 
            this.textBoxBugComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugComment.Location = new System.Drawing.Point(16, 80);
            this.textBoxBugComment.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBugComment.Multiline = true;
            this.textBoxBugComment.Name = "textBoxBugComment";
            this.textBoxBugComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBugComment.Size = new System.Drawing.Size(819, 105);
            this.textBoxBugComment.TabIndex = 2;
            // 
            // textBoxBugSummary
            // 
            this.textBoxBugSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugSummary.Location = new System.Drawing.Point(176, 30);
            this.textBoxBugSummary.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBugSummary.Name = "textBoxBugSummary";
            this.textBoxBugSummary.ReadOnly = true;
            this.textBoxBugSummary.Size = new System.Drawing.Size(659, 22);
            this.textBoxBugSummary.TabIndex = 0;
            // 
            // GollumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(843, 723);
            this.Controls.Add(this.groupBoxBugzilla);
            this.Controls.Add(this.groupBoxReviewBoard);
            this.Controls.Add(this.groupBoxSVN);
            this.Controls.Add(this.groupBoxPostReview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(861, 770);
            this.MinimumSize = new System.Drawing.Size(861, 545);
            this.Name = "GollumForm";
            this.Text = "Gollum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GollumFormFormClosing);
            this.Shown += new System.EventHandler(this.GollumFormShown);
            this.groupBoxPostReview.ResumeLayout(false);
            this.groupBoxSVN.ResumeLayout(false);
            this.groupBoxSVN.PerformLayout();
            this.groupBoxReviewBoard.ResumeLayout(false);
            this.groupBoxReviewBoard.PerformLayout();
            this.groupBoxBugzilla.ResumeLayout(false);
            this.groupBoxBugzilla.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBox textBoxReviewBoardDescription;
        private Button buttonCancel;
        private Label labelCommitMessage;
        private ProgressBar progressBar;
        private Label labelReviewBoardSummary;
        private GroupBox groupBoxPostReview;
        private GroupBox groupBoxSVN;
        private TextBox textBoxRevisionFrom;
        private Label labelTo;
        private Label labelFrom;
        private TextBox textBoxRevisionTo;
        private TextBox textBoxBugsFixed;
        private Label labelBugsFixed;
        private GroupBox groupBoxBugzilla;
        private Label labelBugStatus;
        private Label labelBugComment;
        private TextBox textBoxBugComment;
        private TextBox textBoxBugSummary;
        private ComboBox comboBoxBugResolution;
        private ComboBox comboBoxBugStatus;
        private Label labelDash;
        private Label labelBug;
        private TextBox textBoxBugNumber;
        private TextBox textBoxCommitMessage;
        private TextBox textBoxReviewBoardSummary;
        private Label labelReviewBoardSummaryError;
        private Label labelRevision;
        private Label labelReviewBoardDescription;
        private GroupBox groupBoxReviewBoard;
        private TextBox textBoxSVNRepository;
        private Label labelOf;
        private TextBox textBoxSVNBranch;
        private Label labelBranch;
        private Button buttonPostReview;
        private CheckBox checkBoxUpdateOnlyBugzilla;
    }
}