namespace Aidon.Tools.Gollum.GUI
{
    partial class ProjectSettingsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectSettingsWindow));
            this.textBoxWorkingCopyPath = new System.Windows.Forms.TextBox();
            this.labelInstallationDescription = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.textBoxPathToExe = new System.Windows.Forms.TextBox();
            this.labelPathToGollum = new System.Windows.Forms.Label();
            this.buttonTortoiseSvnSettings = new System.Windows.Forms.Button();
            this.groupBoxInstallation = new System.Windows.Forms.GroupBox();
            this.groupBoxSubmitOldRevision = new System.Windows.Forms.GroupBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.labelRevisionTo = new System.Windows.Forms.Label();
            this.labelRevisionFrom = new System.Windows.Forms.Label();
            this.labelProjectDirectory = new System.Windows.Forms.Label();
            this.textBoxRevisionTo = new System.Windows.Forms.TextBox();
            this.textBoxRevisionFrom = new System.Windows.Forms.TextBox();
            this.textBoxProjectDirectory = new System.Windows.Forms.TextBox();
            this.groupBoxInstallation.SuspendLayout();
            this.groupBoxSubmitOldRevision.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxWorkingCopyPath
            // 
            this.textBoxWorkingCopyPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWorkingCopyPath.Location = new System.Drawing.Point(10, 61);
            this.textBoxWorkingCopyPath.Name = "textBoxWorkingCopyPath";
            this.textBoxWorkingCopyPath.Size = new System.Drawing.Size(377, 20);
            this.textBoxWorkingCopyPath.TabIndex = 0;
            // 
            // labelInstallationDescription
            // 
            this.labelInstallationDescription.Location = new System.Drawing.Point(98, 16);
            this.labelInstallationDescription.Name = "labelInstallationDescription";
            this.labelInstallationDescription.Size = new System.Drawing.Size(279, 42);
            this.labelInstallationDescription.TabIndex = 1;
            this.labelInstallationDescription.Text = "Gollum must be configured with each working copy of a svn repository. Enter worki" +
    "ng copy path to start.";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(393, 61);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelInfo.Location = new System.Drawing.Point(10, 136);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 3;
            // 
            // textBoxPathToExe
            // 
            this.textBoxPathToExe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathToExe.Location = new System.Drawing.Point(13, 110);
            this.textBoxPathToExe.Name = "textBoxPathToExe";
            this.textBoxPathToExe.ReadOnly = true;
            this.textBoxPathToExe.Size = new System.Drawing.Size(458, 20);
            this.textBoxPathToExe.TabIndex = 8;
            // 
            // labelPathToGollum
            // 
            this.labelPathToGollum.AutoSize = true;
            this.labelPathToGollum.Location = new System.Drawing.Point(10, 91);
            this.labelPathToGollum.Name = "labelPathToGollum";
            this.labelPathToGollum.Size = new System.Drawing.Size(97, 13);
            this.labelPathToGollum.TabIndex = 5;
            this.labelPathToGollum.Text = "Path to gollum.exe:";
            // 
            // buttonTortoiseSvnSettings
            // 
            this.buttonTortoiseSvnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTortoiseSvnSettings.Location = new System.Drawing.Point(308, 136);
            this.buttonTortoiseSvnSettings.Name = "buttonTortoiseSvnSettings";
            this.buttonTortoiseSvnSettings.Size = new System.Drawing.Size(161, 23);
            this.buttonTortoiseSvnSettings.TabIndex = 2;
            this.buttonTortoiseSvnSettings.Text = "TortoiseSVN Settings";
            this.buttonTortoiseSvnSettings.UseVisualStyleBackColor = true;
            this.buttonTortoiseSvnSettings.Click += new System.EventHandler(this.ButtonTortoiseSvnSettingsClick);
            // 
            // groupBoxInstallation
            // 
            this.groupBoxInstallation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInstallation.Controls.Add(this.labelInstallationDescription);
            this.groupBoxInstallation.Controls.Add(this.buttonTortoiseSvnSettings);
            this.groupBoxInstallation.Controls.Add(this.textBoxWorkingCopyPath);
            this.groupBoxInstallation.Controls.Add(this.labelPathToGollum);
            this.groupBoxInstallation.Controls.Add(this.buttonOk);
            this.groupBoxInstallation.Controls.Add(this.textBoxPathToExe);
            this.groupBoxInstallation.Controls.Add(this.labelInfo);
            this.groupBoxInstallation.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInstallation.Name = "groupBoxInstallation";
            this.groupBoxInstallation.Size = new System.Drawing.Size(475, 170);
            this.groupBoxInstallation.TabIndex = 0;
            this.groupBoxInstallation.TabStop = false;
            this.groupBoxInstallation.Text = "Installation";
            this.groupBoxInstallation.Enter += new System.EventHandler(this.GroupBoxInstallationEnter);
            // 
            // groupBoxSubmitOldRevision
            // 
            this.groupBoxSubmitOldRevision.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSubmitOldRevision.Controls.Add(this.buttonGo);
            this.groupBoxSubmitOldRevision.Controls.Add(this.labelRevisionTo);
            this.groupBoxSubmitOldRevision.Controls.Add(this.labelRevisionFrom);
            this.groupBoxSubmitOldRevision.Controls.Add(this.labelProjectDirectory);
            this.groupBoxSubmitOldRevision.Controls.Add(this.textBoxRevisionTo);
            this.groupBoxSubmitOldRevision.Controls.Add(this.textBoxRevisionFrom);
            this.groupBoxSubmitOldRevision.Controls.Add(this.textBoxProjectDirectory);
            this.groupBoxSubmitOldRevision.Location = new System.Drawing.Point(12, 188);
            this.groupBoxSubmitOldRevision.Name = "groupBoxSubmitOldRevision";
            this.groupBoxSubmitOldRevision.Size = new System.Drawing.Size(475, 133);
            this.groupBoxSubmitOldRevision.TabIndex = 1;
            this.groupBoxSubmitOldRevision.TabStop = false;
            this.groupBoxSubmitOldRevision.Text = "Submit old revision";
            this.groupBoxSubmitOldRevision.Enter += new System.EventHandler(this.GroupBoxSubmitOldRevisionEnter);
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(393, 101);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 6;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.ButtonGoClick);
            // 
            // labelRevisionTo
            // 
            this.labelRevisionTo.AutoSize = true;
            this.labelRevisionTo.Location = new System.Drawing.Point(6, 77);
            this.labelRevisionTo.Name = "labelRevisionTo";
            this.labelRevisionTo.Size = new System.Drawing.Size(60, 13);
            this.labelRevisionTo.TabIndex = 1;
            this.labelRevisionTo.Text = "Revision to";
            // 
            // labelRevisionFrom
            // 
            this.labelRevisionFrom.AutoSize = true;
            this.labelRevisionFrom.Location = new System.Drawing.Point(6, 51);
            this.labelRevisionFrom.Name = "labelRevisionFrom";
            this.labelRevisionFrom.Size = new System.Drawing.Size(289, 13);
            this.labelRevisionFrom.TabIndex = 1;
            this.labelRevisionFrom.Text = "Revision from (optional, one before \"Revision to\" by default)";
            // 
            // labelProjectDirectory
            // 
            this.labelProjectDirectory.AutoSize = true;
            this.labelProjectDirectory.Location = new System.Drawing.Point(6, 25);
            this.labelProjectDirectory.Name = "labelProjectDirectory";
            this.labelProjectDirectory.Size = new System.Drawing.Size(83, 13);
            this.labelProjectDirectory.TabIndex = 1;
            this.labelProjectDirectory.Text = "Project directory";
            // 
            // textBoxRevisionTo
            // 
            this.textBoxRevisionTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRevisionTo.Location = new System.Drawing.Point(301, 74);
            this.textBoxRevisionTo.Name = "textBoxRevisionTo";
            this.textBoxRevisionTo.Size = new System.Drawing.Size(167, 20);
            this.textBoxRevisionTo.TabIndex = 5;
            // 
            // textBoxRevisionFrom
            // 
            this.textBoxRevisionFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRevisionFrom.Location = new System.Drawing.Point(301, 48);
            this.textBoxRevisionFrom.Name = "textBoxRevisionFrom";
            this.textBoxRevisionFrom.Size = new System.Drawing.Size(167, 20);
            this.textBoxRevisionFrom.TabIndex = 4;
            // 
            // textBoxProjectDirectory
            // 
            this.textBoxProjectDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProjectDirectory.Location = new System.Drawing.Point(95, 22);
            this.textBoxProjectDirectory.Name = "textBoxProjectDirectory";
            this.textBoxProjectDirectory.Size = new System.Drawing.Size(373, 20);
            this.textBoxProjectDirectory.TabIndex = 3;
            // 
            // ProjectSettingsWindow
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 328);
            this.Controls.Add(this.groupBoxSubmitOldRevision);
            this.Controls.Add(this.groupBoxInstallation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectSettingsWindow";
            this.Text = "Project specific settings";
            this.groupBoxInstallation.ResumeLayout(false);
            this.groupBoxInstallation.PerformLayout();
            this.groupBoxSubmitOldRevision.ResumeLayout(false);
            this.groupBoxSubmitOldRevision.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWorkingCopyPath;
        private System.Windows.Forms.Label labelInstallationDescription;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TextBox textBoxPathToExe;
        private System.Windows.Forms.Label labelPathToGollum;
        private System.Windows.Forms.Button buttonTortoiseSvnSettings;
        private System.Windows.Forms.GroupBox groupBoxInstallation;
        private System.Windows.Forms.GroupBox groupBoxSubmitOldRevision;
        private System.Windows.Forms.Label labelProjectDirectory;
        private System.Windows.Forms.TextBox textBoxProjectDirectory;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label labelRevisionTo;
        private System.Windows.Forms.Label labelRevisionFrom;
        private System.Windows.Forms.TextBox textBoxRevisionTo;
        private System.Windows.Forms.TextBox textBoxRevisionFrom;
    }
}