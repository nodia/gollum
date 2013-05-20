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

        private void ButtonOkClick(object sender, EventArgs e)
        {
            labelInfo.Text = "";
            string workingCopyPath = textBoxWorkingCopyPath.Text;

            if (!Directory.Exists(workingCopyPath))
            {
                labelInfo.Text = "The path does not exist";
                return;
            }

            if (!Directory.Exists(Path.Combine(workingCopyPath, ".svn")))
            {
                labelInfo.Text = "The path is not a working copy. There is no .svn directory.";
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
                labelInfo.Text = "The project specific settings file " + ProjectSettings.DefaultFileName + " created.";
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

        private static void OpenFileInEditor(string workingCopyPath, string settingsFilePath)
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

        private void ButtonGoClick(object sender, EventArgs e)
        {
            int revisionTo;
            int revisionFrom = -1;

            if (string.IsNullOrWhiteSpace(textBoxProjectDirectory.Text) || !Directory.Exists(textBoxProjectDirectory.Text) ||
                string.IsNullOrWhiteSpace(textBoxRevisionTo.Text) || !int.TryParse(textBoxRevisionTo.Text, out revisionTo) ||
                (!string.IsNullOrWhiteSpace(textBoxRevisionFrom.Text) && !int.TryParse(textBoxRevisionFrom.Text, out revisionFrom)) ||
                revisionFrom >= revisionTo
                )
            {
                MessageBox.Show("Fill fields better");
                return;
            }

            var cwd = textBoxProjectDirectory.Text;

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

        private void GroupBoxSubmitOldRevisionEnter(object sender, EventArgs e)
        {
            AcceptButton = buttonGo;
        }

        private void GroupBoxInstallationEnter(object sender, EventArgs e)
        {
            AcceptButton = buttonOk;
        }
    }
}
