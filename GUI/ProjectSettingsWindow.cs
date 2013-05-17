using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum.GUI
{
    public partial class ProjectSettingsWindow : Form
    {
        public ProjectSettings ProjectSettings { get; set; }
        public SubversionArguments SubversionArguments { get; set; }

        public ProjectSettingsWindow()
        {
            InitializeComponent();

            textBoxWorkingCopyPath.Text = Directory.GetCurrentDirectory();
            textBoxWorkingCopyPath.SelectAll();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            infoLabel.Text = "";
            string workingCopyPath = textBoxWorkingCopyPath.Text;

            if (!Directory.Exists(workingCopyPath))
            {
                infoLabel.Text = "The path does not exist";
                return;
            }

            if (!Directory.Exists(Path.Combine(workingCopyPath, ".svn")))
            {
                infoLabel.Text = "The path is not a working copy. There is no .svn directory.";
                return;
            }

            textBoxPathToExe.Text = Application.ExecutablePath;
            textBoxPathToExe.Focus();
            textBoxPathToExe.SelectAll();

            string settingsFilePath = Path.Combine(workingCopyPath, ProjectSettings.DefaultFileName);
            if (!File.Exists(settingsFilePath))
            {
                var settings = new ProjectSettings
                {
                    RepositoryBasePath = "/",
                    ReviewBoardGroup = "ExampleReviewBoardGroup",
                    ReviewBoardRepositoryName = "ExampleReviewBoardRepositoryName"
                };
                ProjectSettings.Save(settings, Path.Combine(workingCopyPath, ProjectSettings.DefaultFileName));
                infoLabel.Text = "The project specific settings file " + ProjectSettings.DefaultFileName + " created.";
                OpenFileInEditor(workingCopyPath, settingsFilePath);
                
                LaunchTortoiseSettings();
            }
            else
            {
                OpenFileInEditor(workingCopyPath, settingsFilePath);
            }
        }

        private static void LaunchTortoiseSettings()
        {
            var tortoiseSettings = new Process
            {
                StartInfo =
                    {
                        FileName = "tortoiseproc",
                        Arguments = "/command:settings",
                        UseShellExecute = true
                    }
            };
            tortoiseSettings.Start();
        }

        private void OpenFileInEditor(string workingCopyPath, string settingsFilePath)
        {
            var notepad = new Process
            {
                StartInfo =
                    {
                        FileName = "notepad",
                        Arguments = settingsFilePath,
                        WorkingDirectory = workingCopyPath,
                        UseShellExecute = true
                    }
            };
            notepad.Start();
        }

        private void ButtonTortoiseSvnSettingsClick(object sender, EventArgs e)
        {
            LaunchTortoiseSettings();
        }

        private void SubmitClick(object sender, EventArgs e)
        {
            int revisionTo;
            int revisionFrom = -1;

            if (string.IsNullOrWhiteSpace(tbProjectDir.Text) || !Directory.Exists(tbProjectDir.Text) ||
                string.IsNullOrWhiteSpace(tbRevTo.Text) || !int.TryParse(tbRevTo.Text, out revisionTo) ||
                (!string.IsNullOrWhiteSpace(tbRevFrom.Text) && !int.TryParse(tbRevFrom.Text, out revisionFrom)) ||
                revisionFrom >= revisionTo
                )
            {
                MessageBox.Show("Fill fields better");
                return;
            }

            var cwd = tbProjectDir.Text;

            string projectRootDirectory;
            try
            {
                projectRootDirectory = Program.FindProjectRootDirectory(cwd, ProjectSettings.DefaultFileName);
            }
            catch
            {
                MessageBox.Show("Directory not gollum-compatible.");
                return;
            }

            var subversionArguments = new SubversionArguments
            {
                RevisionTo = revisionTo,
                RevisionFrom = revisionFrom,
                Cwd = cwd,
                LocalProjectRootDirectory = projectRootDirectory
            };

            try
            {
                subversionArguments.Message = SvnPatchCreator.GetMessageForRevision(subversionArguments);
            }
            catch
            {
                MessageBox.Show("Unable to get message for revision.");
                return;
            }

            ProjectSettings = ProjectSettings.Load(Path.Combine(subversionArguments.LocalProjectRootDirectory, ProjectSettings.DefaultFileName));
            SubversionArguments = subversionArguments;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void GroupBox2Enter(object sender, EventArgs e)
        {
            AcceptButton = submit;
        }

        private void GroupBox1Enter(object sender, EventArgs e)
        {
            AcceptButton = button1;
        }
    }
}
