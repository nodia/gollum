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
            this.labelRevision.Location = new System.Drawing.Point(12, 16);
            this.labelRevision.Name = "labelRevision";
            this.labelRevision.Size = new System.Drawing.Size(51, 13);
            this.labelRevision.TabIndex = 0;
            this.labelRevision.Text = "Revision";
            // 
            // labelReviewBoardDescription
            // 
            this.labelReviewBoardDescription.Location = new System.Drawing.Point(12, 89);
            this.labelReviewBoardDescription.Name = "labelReviewBoardDescription";
            this.labelReviewBoardDescription.Size = new System.Drawing.Size(284, 13);
            this.labelReviewBoardDescription.TabIndex = 2;
            this.labelReviewBoardDescription.Text = "Review Board description";
            // 
            // textBoxReviewBoardDescription
            // 
            this.textBoxReviewBoardDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReviewBoardDescription.Location = new System.Drawing.Point(12, 105);
            this.textBoxReviewBoardDescription.Multiline = true;
            this.textBoxReviewBoardDescription.Name = "textBoxReviewBoardDescription";
            this.textBoxReviewBoardDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReviewBoardDescription.Size = new System.Drawing.Size(617, 62);
            this.textBoxReviewBoardDescription.TabIndex = 2;
            this.textBoxReviewBoardDescription.TextChanged += new System.EventHandler(this.TextBoxReviewBoardDescriptionTextChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonCancel.Location = new System.Drawing.Point(317, 16);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(314, 47);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "No review";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // labelReviewBoardSummaryError
            // 
            this.labelReviewBoardSummaryError.ForeColor = System.Drawing.Color.Red;
            this.labelReviewBoardSummaryError.Location = new System.Drawing.Point(142, 21);
            this.labelReviewBoardSummaryError.Name = "labelReviewBoardSummaryError";
            this.labelReviewBoardSummaryError.Size = new System.Drawing.Size(342, 13);
            this.labelReviewBoardSummaryError.TabIndex = 6;
            this.labelReviewBoardSummaryError.Text = "Review board summary and description are required!";
            this.labelReviewBoardSummaryError.Visible = false;
            // 
            // textBoxReviewBoardSummary
            // 
            this.textBoxReviewBoardSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReviewBoardSummary.Location = new System.Drawing.Point(12, 37);
            this.textBoxReviewBoardSummary.Multiline = true;
            this.textBoxReviewBoardSummary.Name = "textBoxReviewBoardSummary";
            this.textBoxReviewBoardSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReviewBoardSummary.Size = new System.Drawing.Size(617, 49);
            this.textBoxReviewBoardSummary.TabIndex = 5;
            this.textBoxReviewBoardSummary.TextChanged += new System.EventHandler(this.TextBoxReviewBoardSummaryTextChanged);
            // 
            // textBoxBugsFixed
            // 
            this.textBoxBugsFixed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugsFixed.Location = new System.Drawing.Point(70, 173);
            this.textBoxBugsFixed.Name = "textBoxBugsFixed";
            this.textBoxBugsFixed.Size = new System.Drawing.Size(417, 20);
            this.textBoxBugsFixed.TabIndex = 4;
            this.textBoxBugsFixed.TextChanged += new System.EventHandler(this.TextBoxBugsFixedTextChanged);
            // 
            // labelBugsFixed
            // 
            this.labelBugsFixed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBugsFixed.Location = new System.Drawing.Point(10, 176);
            this.labelBugsFixed.Name = "labelBugsFixed";
            this.labelBugsFixed.Size = new System.Drawing.Size(60, 16);
            this.labelBugsFixed.TabIndex = 3;
            this.labelBugsFixed.Text = "Bugs fixed";
            // 
            // labelReviewBoardSummary
            // 
            this.labelReviewBoardSummary.Location = new System.Drawing.Point(12, 21);
            this.labelReviewBoardSummary.Name = "labelReviewBoardSummary";
            this.labelReviewBoardSummary.Size = new System.Drawing.Size(126, 20);
            this.labelReviewBoardSummary.TabIndex = 2;
            this.labelReviewBoardSummary.Text = "Review Board summary";
            // 
            // labelCommitMessage
            // 
            this.labelCommitMessage.Location = new System.Drawing.Point(12, 42);
            this.labelCommitMessage.Name = "labelCommitMessage";
            this.labelCommitMessage.Size = new System.Drawing.Size(107, 19);
            this.labelCommitMessage.TabIndex = 2;
            this.labelCommitMessage.Text = "Commit message";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(3, 63);
            this.progressBar.MarqueeAnimationSpeed = 30;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(628, 23);
            this.progressBar.TabIndex = 10;
            // 
            // groupBoxPostReview
            // 
            this.groupBoxPostReview.Controls.Add(this.buttonPostReview);
            this.groupBoxPostReview.Controls.Add(this.buttonCancel);
            this.groupBoxPostReview.Controls.Add(this.progressBar);
            this.groupBoxPostReview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxPostReview.Location = new System.Drawing.Point(0, 498);
            this.groupBoxPostReview.Name = "groupBoxPostReview";
            this.groupBoxPostReview.Size = new System.Drawing.Size(634, 89);
            this.groupBoxPostReview.TabIndex = 11;
            this.groupBoxPostReview.TabStop = false;
            this.groupBoxPostReview.Text = "Post review?";
            // 
            // buttonPostReview
            // 
            this.buttonPostReview.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonPostReview.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPostReview.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonPostReview.Location = new System.Drawing.Point(3, 16);
            this.buttonPostReview.Name = "buttonPostReview";
            this.buttonPostReview.Size = new System.Drawing.Size(314, 47);
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
            this.groupBoxSVN.Name = "groupBoxSVN";
            this.groupBoxSVN.Size = new System.Drawing.Size(634, 120);
            this.groupBoxSVN.TabIndex = 12;
            this.groupBoxSVN.TabStop = false;
            this.groupBoxSVN.Text = "SVN";
            // 
            // textBoxSVNBranch
            // 
            this.textBoxSVNBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSVNBranch.Location = new System.Drawing.Point(486, 12);
            this.textBoxSVNBranch.Name = "textBoxSVNBranch";
            this.textBoxSVNBranch.ReadOnly = true;
            this.textBoxSVNBranch.Size = new System.Drawing.Size(142, 20);
            this.textBoxSVNBranch.TabIndex = 12;
            // 
            // labelBranch
            // 
            this.labelBranch.Location = new System.Drawing.Point(440, 16);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(44, 13);
            this.labelBranch.TabIndex = 11;
            this.labelBranch.Text = "branch";
            // 
            // textBoxSVNRepository
            // 
            this.textBoxSVNRepository.Location = new System.Drawing.Point(289, 13);
            this.textBoxSVNRepository.Name = "textBoxSVNRepository";
            this.textBoxSVNRepository.ReadOnly = true;
            this.textBoxSVNRepository.Size = new System.Drawing.Size(145, 20);
            this.textBoxSVNRepository.TabIndex = 10;
            // 
            // labelOf
            // 
            this.labelOf.Location = new System.Drawing.Point(260, 16);
            this.labelOf.Name = "labelOf";
            this.labelOf.Size = new System.Drawing.Size(23, 13);
            this.labelOf.TabIndex = 9;
            this.labelOf.Text = "of";
            // 
            // textBoxCommitMessage
            // 
            this.textBoxCommitMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCommitMessage.Location = new System.Drawing.Point(101, 39);
            this.textBoxCommitMessage.Multiline = true;
            this.textBoxCommitMessage.Name = "textBoxCommitMessage";
            this.textBoxCommitMessage.ReadOnly = true;
            this.textBoxCommitMessage.Size = new System.Drawing.Size(527, 70);
            this.textBoxCommitMessage.TabIndex = 8;
            // 
            // labelTo
            // 
            this.labelTo.Location = new System.Drawing.Point(165, 15);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(19, 13);
            this.labelTo.TabIndex = 7;
            this.labelTo.Text = "to";
            this.labelTo.Visible = false;
            // 
            // labelFrom
            // 
            this.labelFrom.Location = new System.Drawing.Point(69, 16);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(28, 13);
            this.labelFrom.TabIndex = 6;
            this.labelFrom.Text = "from";
            this.labelFrom.Visible = false;
            // 
            // textBoxRevisionTo
            // 
            this.textBoxRevisionTo.Location = new System.Drawing.Point(190, 12);
            this.textBoxRevisionTo.Name = "textBoxRevisionTo";
            this.textBoxRevisionTo.ReadOnly = true;
            this.textBoxRevisionTo.Size = new System.Drawing.Size(54, 20);
            this.textBoxRevisionTo.TabIndex = 5;
            this.textBoxRevisionTo.Visible = false;
            // 
            // textBoxRevisionFrom
            // 
            this.textBoxRevisionFrom.Location = new System.Drawing.Point(101, 12);
            this.textBoxRevisionFrom.Name = "textBoxRevisionFrom";
            this.textBoxRevisionFrom.ReadOnly = true;
            this.textBoxRevisionFrom.Size = new System.Drawing.Size(56, 20);
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
            this.groupBoxReviewBoard.Location = new System.Drawing.Point(0, 120);
            this.groupBoxReviewBoard.Name = "groupBoxReviewBoard";
            this.groupBoxReviewBoard.Size = new System.Drawing.Size(634, 195);
            this.groupBoxReviewBoard.TabIndex = 9;
            this.groupBoxReviewBoard.TabStop = false;
            this.groupBoxReviewBoard.Text = "Review Board";
            // 
            // checkBoxUpdateOnlyBugzilla
            // 
            this.checkBoxUpdateOnlyBugzilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUpdateOnlyBugzilla.AutoSize = true;
            this.checkBoxUpdateOnlyBugzilla.Enabled = false;
            this.checkBoxUpdateOnlyBugzilla.Location = new System.Drawing.Point(505, 175);
            this.checkBoxUpdateOnlyBugzilla.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxUpdateOnlyBugzilla.Name = "checkBoxUpdateOnlyBugzilla";
            this.checkBoxUpdateOnlyBugzilla.Size = new System.Drawing.Size(109, 17);
            this.checkBoxUpdateOnlyBugzilla.TabIndex = 7;
            this.checkBoxUpdateOnlyBugzilla.Text = "Update only bugs";
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
            this.groupBoxBugzilla.Location = new System.Drawing.Point(0, 315);
            this.groupBoxBugzilla.Name = "groupBoxBugzilla";
            this.groupBoxBugzilla.Size = new System.Drawing.Size(634, 183);
            this.groupBoxBugzilla.TabIndex = 10;
            this.groupBoxBugzilla.TabStop = false;
            this.groupBoxBugzilla.Text = "Bugzilla";
            // 
            // labelDash
            // 
            this.labelDash.Location = new System.Drawing.Point(121, 27);
            this.labelDash.Name = "labelDash";
            this.labelDash.Size = new System.Drawing.Size(10, 13);
            this.labelDash.TabIndex = 8;
            this.labelDash.Text = "-";
            // 
            // labelBug
            // 
            this.labelBug.Location = new System.Drawing.Point(12, 27);
            this.labelBug.Name = "labelBug";
            this.labelBug.Size = new System.Drawing.Size(28, 19);
            this.labelBug.TabIndex = 7;
            this.labelBug.Text = "Bug";
            // 
            // textBoxBugNumber
            // 
            this.textBoxBugNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugNumber.Location = new System.Drawing.Point(41, 24);
            this.textBoxBugNumber.Name = "textBoxBugNumber";
            this.textBoxBugNumber.ReadOnly = true;
            this.textBoxBugNumber.Size = new System.Drawing.Size(80, 20);
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
            this.comboBoxBugResolution.Location = new System.Drawing.Point(384, 157);
            this.comboBoxBugResolution.Name = "comboBoxBugResolution";
            this.comboBoxBugResolution.Size = new System.Drawing.Size(245, 21);
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
            this.comboBoxBugStatus.Location = new System.Drawing.Point(132, 157);
            this.comboBoxBugStatus.Name = "comboBoxBugStatus";
            this.comboBoxBugStatus.Size = new System.Drawing.Size(246, 21);
            this.comboBoxBugStatus.TabIndex = 4;
            this.comboBoxBugStatus.SelectedValueChanged += new System.EventHandler(this.ComboBoxBugStatusSelectedValueChanged);
            // 
            // labelBugStatus
            // 
            this.labelBugStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBugStatus.Location = new System.Drawing.Point(12, 160);
            this.labelBugStatus.Name = "labelBugStatus";
            this.labelBugStatus.Size = new System.Drawing.Size(107, 16);
            this.labelBugStatus.TabIndex = 3;
            this.labelBugStatus.Text = "Bug status";
            // 
            // labelBugComment
            // 
            this.labelBugComment.Location = new System.Drawing.Point(12, 46);
            this.labelBugComment.Name = "labelBugComment";
            this.labelBugComment.Size = new System.Drawing.Size(112, 16);
            this.labelBugComment.TabIndex = 2;
            this.labelBugComment.Text = "Bugzilla comment";
            // 
            // textBoxBugComment
            // 
            this.textBoxBugComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugComment.Location = new System.Drawing.Point(12, 65);
            this.textBoxBugComment.Multiline = true;
            this.textBoxBugComment.Name = "textBoxBugComment";
            this.textBoxBugComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBugComment.Size = new System.Drawing.Size(617, 86);
            this.textBoxBugComment.TabIndex = 2;
            // 
            // textBoxBugSummary
            // 
            this.textBoxBugSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugSummary.Location = new System.Drawing.Point(132, 24);
            this.textBoxBugSummary.Name = "textBoxBugSummary";
            this.textBoxBugSummary.ReadOnly = true;
            this.textBoxBugSummary.Size = new System.Drawing.Size(497, 20);
            this.textBoxBugSummary.TabIndex = 0;
            // 
            // GollumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(634, 587);
            this.Controls.Add(this.groupBoxBugzilla);
            this.Controls.Add(this.groupBoxReviewBoard);
            this.Controls.Add(this.groupBoxSVN);
            this.Controls.Add(this.groupBoxPostReview);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
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