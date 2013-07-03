using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum.GUI
{
    public partial class ProjectSettingsWindow : Form
    {
        private readonly ToolTip _projectToolTip;

        public ProjectSettings ProjectSettings { get; private set; }
        public SubversionArguments SubversionArguments { get; private set; }

        public ProjectSettingsWindow()
        {
            InitializeComponent();

            textBoxWorkingCopyPath.Text = Directory.GetCurrentDirectory();
            textBoxWorkingCopyPath.SelectAll();

            _projectToolTip = new ToolTip
            {
                IsBalloon = false, 
                UseAnimation = true, 
                ToolTipIcon = ToolTipIcon.Info, 
                AutoPopDelay = 5000, 
                InitialDelay = 500, 
                ToolTipTitle = "Tip"
            };
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

        private async void ButtonGoClick(object sender, EventArgs e)
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

            buttonGo.Enabled = false;

            try
            {
                subversionArguments.Message = await SvnPatchCreator.GetMessageForRevision(subversionArguments);
                ProjectSettings = ProjectSettings.Load(Path.Combine(subversionArguments.LocalProjectRootDirectory, ProjectSettings.DefaultFileName));
                SubversionArguments = subversionArguments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get message for revision: " + ex.Message);
                buttonGo.Enabled = true;
                return;
            }

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

        private void ProjectDirectoryDoubleClick(object sender, EventArgs e)
        {
            using (var openDirDialog = new FolderBrowserDialog())
            {
                openDirDialog.ShowNewFolderButton = false;
                openDirDialog.Description = "Select project directory";
                
                var result = openDirDialog.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    return;
                }

                var dir = openDirDialog.SelectedPath;
                if (String.IsNullOrEmpty(dir))
                {
                    return;
                }

                if (!dir.EndsWith("\\", StringComparison.Ordinal))
                {
                    dir += "\\";
                }

                textBoxProjectDirectory.Text = dir;
            }
        }

        private void TextBoxProjectDirectoryMouseEnter(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxProjectDirectory.Text))
            {
                _projectToolTip.Show("Double click to browse for folder.", textBoxProjectDirectory);
            }
        }
    }
}
