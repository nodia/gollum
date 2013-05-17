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
            this.buttonPostReview = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelReviewBoardSummaryError = new System.Windows.Forms.Label();
            this.textBoxReviewBoardSummary = new System.Windows.Forms.TextBox();
            this.textBoxBugsFixed = new System.Windows.Forms.TextBox();
            this.labelBugsFixed = new System.Windows.Forms.Label();
            this.labelReviewBoardSummary = new System.Windows.Forms.Label();
            this.labelCommitMessage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxPostReview = new System.Windows.Forms.GroupBox();
            this.groupBoxSVN = new System.Windows.Forms.GroupBox();
            this.textBoxCommitMessage = new System.Windows.Forms.TextBox();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxRevisionTo = new System.Windows.Forms.TextBox();
            this.textBoxRevisionFrom = new System.Windows.Forms.TextBox();
            this.groupBoxReviewBoard = new System.Windows.Forms.GroupBox();
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
            this.labelOf = new System.Windows.Forms.Label();
            this.textBoxSVNRepository = new System.Windows.Forms.TextBox();
            this.labelBranch = new System.Windows.Forms.Label();
            this.textBoxSVNBranch = new System.Windows.Forms.TextBox();
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
            this.labelReviewBoardDescription.Location = new System.Drawing.Point(12, 109);
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
            this.textBoxReviewBoardDescription.Location = new System.Drawing.Point(12, 125);
            this.textBoxReviewBoardDescription.Multiline = true;
            this.textBoxReviewBoardDescription.Name = "textBoxReviewBoardDescription";
            this.textBoxReviewBoardDescription.Size = new System.Drawing.Size(617, 153);
            this.textBoxReviewBoardDescription.TabIndex = 2;
            // 
            // buttonPostReview
            // 
            this.buttonPostReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPostReview.Location = new System.Drawing.Point(3, 16);
            this.buttonPostReview.Name = "buttonPostReview";
            this.buttonPostReview.Size = new System.Drawing.Size(306, 42);
            this.buttonPostReview.TabIndex = 7;
            this.buttonPostReview.Text = "Post review";
            this.buttonPostReview.Click += new System.EventHandler(this.ButtonPostReviewClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCancel.Location = new System.Drawing.Point(309, 16);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(322, 42);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "No review";
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // labelReviewBoardSummaryError
            // 
            this.labelReviewBoardSummaryError.ForeColor = System.Drawing.Color.Red;
            this.labelReviewBoardSummaryError.Location = new System.Drawing.Point(142, 21);
            this.labelReviewBoardSummaryError.Name = "labelReviewBoardSummaryError";
            this.labelReviewBoardSummaryError.Size = new System.Drawing.Size(156, 13);
            this.labelReviewBoardSummaryError.TabIndex = 6;
            this.labelReviewBoardSummaryError.Text = "Line breaks are not allowed!";
            this.labelReviewBoardSummaryError.Visible = false;
            // 
            // textBoxReviewBoardSummary
            // 
            this.textBoxReviewBoardSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReviewBoardSummary.Location = new System.Drawing.Point(12, 37);
            this.textBoxReviewBoardSummary.Multiline = true;
            this.textBoxReviewBoardSummary.Name = "textBoxReviewBoardSummary";
            this.textBoxReviewBoardSummary.Size = new System.Drawing.Size(617, 66);
            this.textBoxReviewBoardSummary.TabIndex = 5;
            this.textBoxReviewBoardSummary.TextChanged += new System.EventHandler(this.TextBoxReviewBoardSummaryTextChanged);
            // 
            // textBoxBugsFixed
            // 
            this.textBoxBugsFixed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBugsFixed.Location = new System.Drawing.Point(103, 289);
            this.textBoxBugsFixed.Name = "textBoxBugsFixed";
            this.textBoxBugsFixed.Size = new System.Drawing.Size(526, 20);
            this.textBoxBugsFixed.TabIndex = 4;
            this.textBoxBugsFixed.TextChanged += new System.EventHandler(this.TextBoxBugsFixedTextChanged);
            // 
            // labelBugsFixed
            // 
            this.labelBugsFixed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBugsFixed.Location = new System.Drawing.Point(12, 292);
            this.labelBugsFixed.Name = "labelBugsFixed";
            this.labelBugsFixed.Size = new System.Drawing.Size(68, 13);
            this.labelBugsFixed.TabIndex = 3;
            this.labelBugsFixed.Text = "Bugs fixed";
            // 
            // labelReviewBoardSummary
            // 
            this.labelReviewBoardSummary.Location = new System.Drawing.Point(12, 21);
            this.labelReviewBoardSummary.Name = "labelReviewBoardSummary";
            this.labelReviewBoardSummary.Size = new System.Drawing.Size(126, 13);
            this.labelReviewBoardSummary.TabIndex = 2;
            this.labelReviewBoardSummary.Text = "Review Board summary";
            // 
            // labelCommitMessage
            // 
            this.labelCommitMessage.Location = new System.Drawing.Point(12, 42);
            this.labelCommitMessage.Name = "labelCommitMessage";
            this.labelCommitMessage.Size = new System.Drawing.Size(107, 13);
            this.labelCommitMessage.TabIndex = 2;
            this.labelCommitMessage.Text = "Commit message";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(3, 58);
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
            this.groupBoxPostReview.Location = new System.Drawing.Point(0, 648);
            this.groupBoxPostReview.MinimumSize = new System.Drawing.Size(634, 84);
            this.groupBoxPostReview.Name = "groupBoxPostReview";
            this.groupBoxPostReview.Size = new System.Drawing.Size(634, 84);
            this.groupBoxPostReview.TabIndex = 11;
            this.groupBoxPostReview.TabStop = false;
            this.groupBoxPostReview.Text = "Post review?";
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
            this.groupBoxSVN.MaximumSize = new System.Drawing.Size(634, 120);
            this.groupBoxSVN.MinimumSize = new System.Drawing.Size(634, 120);
            this.groupBoxSVN.Name = "groupBoxSVN";
            this.groupBoxSVN.Size = new System.Drawing.Size(634, 120);
            this.groupBoxSVN.TabIndex = 12;
            this.groupBoxSVN.TabStop = false;
            this.groupBoxSVN.Text = "SVN";
            // 
            // textBoxCommitMessage
            // 
            this.textBoxCommitMessage.Location = new System.Drawing.Point(131, 39);
            this.textBoxCommitMessage.Multiline = true;
            this.textBoxCommitMessage.Name = "textBoxCommitMessage";
            this.textBoxCommitMessage.ReadOnly = true;
            this.textBoxCommitMessage.Size = new System.Drawing.Size(497, 70);
            this.textBoxCommitMessage.TabIndex = 8;
            // 
            // labelTo
            // 
            this.labelTo.Location = new System.Drawing.Point(186, 16);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(19, 13);
            this.labelTo.TabIndex = 7;
            this.labelTo.Text = "to";
            this.labelTo.Visible = false;
            // 
            // labelFrom
            // 
            this.labelFrom.Location = new System.Drawing.Point(98, 16);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(28, 13);
            this.labelFrom.TabIndex = 6;
            this.labelFrom.Text = "from";
            this.labelFrom.Visible = false;
            // 
            // textBoxRevisionTo
            // 
            this.textBoxRevisionTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRevisionTo.Location = new System.Drawing.Point(204, 13);
            this.textBoxRevisionTo.Name = "textBoxRevisionTo";
            this.textBoxRevisionTo.ReadOnly = true;
            this.textBoxRevisionTo.Size = new System.Drawing.Size(50, 20);
            this.textBoxRevisionTo.TabIndex = 5;
            this.textBoxRevisionTo.Visible = false;
            // 
            // textBoxRevisionFrom
            // 
            this.textBoxRevisionFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRevisionFrom.Location = new System.Drawing.Point(132, 13);
            this.textBoxRevisionFrom.Name = "textBoxRevisionFrom";
            this.textBoxRevisionFrom.ReadOnly = true;
            this.textBoxRevisionFrom.Size = new System.Drawing.Size(50, 20);
            this.textBoxRevisionFrom.TabIndex = 4;
            // 
            // groupBoxReviewBoard
            // 
            this.groupBoxReviewBoard.Controls.Add(this.textBoxBugsFixed);
            this.groupBoxReviewBoard.Controls.Add(this.textBoxReviewBoardSummary);
            this.groupBoxReviewBoard.Controls.Add(this.labelBugsFixed);
            this.groupBoxReviewBoard.Controls.Add(this.labelReviewBoardSummaryError);
            this.groupBoxReviewBoard.Controls.Add(this.textBoxReviewBoardDescription);
            this.groupBoxReviewBoard.Controls.Add(this.labelReviewBoardDescription);
            this.groupBoxReviewBoard.Controls.Add(this.labelReviewBoardSummary);
            this.groupBoxReviewBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxReviewBoard.Location = new System.Drawing.Point(0, 120);
            this.groupBoxReviewBoard.MaximumSize = new System.Drawing.Size(640, 315);
            this.groupBoxReviewBoard.MinimumSize = new System.Drawing.Size(630, 315);
            this.groupBoxReviewBoard.Name = "groupBoxReviewBoard";
            this.groupBoxReviewBoard.Size = new System.Drawing.Size(634, 315);
            this.groupBoxReviewBoard.TabIndex = 9;
            this.groupBoxReviewBoard.TabStop = false;
            this.groupBoxReviewBoard.Text = "Review Board";
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
            this.groupBoxBugzilla.Location = new System.Drawing.Point(0, 435);
            this.groupBoxBugzilla.Name = "groupBoxBugzilla";
            this.groupBoxBugzilla.Size = new System.Drawing.Size(634, 213);
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
            this.labelBug.Size = new System.Drawing.Size(28, 13);
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
            this.textBoxBugNumber.Size = new System.Drawing.Size(78, 20);
            this.textBoxBugNumber.TabIndex = 6;
            // 
            // comboBoxBugResolution
            // 
            this.comboBoxBugResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBugResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBugResolution.Enabled = false;
            this.comboBoxBugResolution.Items.AddRange(new object[] {
            "FIXED",
            "INVALID",
            "WONTFIX",
            "DUPLICATE",
            "WORKSFORME"});
            this.comboBoxBugResolution.Location = new System.Drawing.Point(384, 186);
            this.comboBoxBugResolution.Name = "comboBoxBugResolution";
            this.comboBoxBugResolution.Size = new System.Drawing.Size(245, 21);
            this.comboBoxBugResolution.TabIndex = 5;
            // 
            // comboBoxBugStatus
            // 
            this.comboBoxBugStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBugStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBugStatus.Items.AddRange(new object[] {
            "UNCONFIRMED",
            "CONFIRMED",
            "IN_PROGRESS",
            "RESOLVED",
            "VERIFIED"});
            this.comboBoxBugStatus.Location = new System.Drawing.Point(132, 186);
            this.comboBoxBugStatus.Name = "comboBoxBugStatus";
            this.comboBoxBugStatus.Size = new System.Drawing.Size(246, 21);
            this.comboBoxBugStatus.TabIndex = 4;
            this.comboBoxBugStatus.SelectedValueChanged += new System.EventHandler(this.ComboBoxBugStatusSelectedValueChanged);
            // 
            // labelBugStatus
            // 
            this.labelBugStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBugStatus.Location = new System.Drawing.Point(12, 189);
            this.labelBugStatus.Name = "labelBugStatus";
            this.labelBugStatus.Size = new System.Drawing.Size(107, 13);
            this.labelBugStatus.TabIndex = 3;
            this.labelBugStatus.Text = "Bug status";
            // 
            // labelBugComment
            // 
            this.labelBugComment.Location = new System.Drawing.Point(12, 49);
            this.labelBugComment.Name = "labelBugComment";
            this.labelBugComment.Size = new System.Drawing.Size(112, 13);
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
            this.textBoxBugComment.Size = new System.Drawing.Size(617, 118);
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
            // labelOf
            // 
            this.labelOf.Location = new System.Drawing.Point(260, 16);
            this.labelOf.Name = "labelOf";
            this.labelOf.Size = new System.Drawing.Size(23, 13);
            this.labelOf.TabIndex = 9;
            this.labelOf.Text = "of";
            // 
            // textBoxSVNRepository
            // 
            this.textBoxSVNRepository.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSVNRepository.Location = new System.Drawing.Point(289, 13);
            this.textBoxSVNRepository.Name = "textBoxSVNRepository";
            this.textBoxSVNRepository.ReadOnly = true;
            this.textBoxSVNRepository.Size = new System.Drawing.Size(145, 20);
            this.textBoxSVNRepository.TabIndex = 10;
            // 
            // labelBranch
            // 
            this.labelBranch.Location = new System.Drawing.Point(440, 16);
            this.labelBranch.Name = "labelBranch";
            this.labelBranch.Size = new System.Drawing.Size(44, 13);
            this.labelBranch.TabIndex = 11;
            this.labelBranch.Text = "branch";
            // 
            // textBoxSVNBranch
            // 
            this.textBoxSVNBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSVNBranch.Location = new System.Drawing.Point(486, 12);
            this.textBoxSVNBranch.Name = "textBoxSVNBranch";
            this.textBoxSVNBranch.ReadOnly = true;
            this.textBoxSVNBranch.Size = new System.Drawing.Size(143, 20);
            this.textBoxSVNBranch.TabIndex = 12;
            // 
            // GollumForm
            // 
            this.AcceptButton = this.buttonPostReview;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(634, 732);
            this.Controls.Add(this.groupBoxBugzilla);
            this.Controls.Add(this.groupBoxReviewBoard);
            this.Controls.Add(this.groupBoxSVN);
            this.Controls.Add(this.groupBoxPostReview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(650, 770);
            this.MinimumSize = new System.Drawing.Size(650, 567);
            this.Name = "GollumForm";
            this.Text = "Gollum";
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
        private Button buttonPostReview;
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
    }
}