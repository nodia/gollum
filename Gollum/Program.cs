using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Net;
using Aidon.Tools.Gollum.GUI;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProjectSettings projectSettings;
            SubversionArguments subversionArguments;

            if (args.Length == 0)
            {
                var projectSettingsWindow = new ProjectSettingsWindow();
                if (projectSettingsWindow.ShowDialog() == DialogResult.OK)
                {
                    projectSettings = projectSettingsWindow.ProjectSettings;
                    subversionArguments = projectSettingsWindow.SubversionArguments;    
                }
                else
                {
                    return 1;    
                }
            }
            else
            {
                try
                {
                    if (args[0].Trim().ToLowerInvariant() == "-r")
                    {
                        subversionArguments = ReadCommandLineArguments(args);
                    }
                    else if (args.Length == 6)
                    {
                        subversionArguments = ReadTortoiseArguments(args);

                        if (!String.IsNullOrEmpty(subversionArguments.Error))
                        {
                            Console.Error.WriteLine("Subversion error:");
                            Console.Error.WriteLine(subversionArguments.Error);
                            return 1;
                        }
                    }
                    else
                    {
                        ShowHelp();
                        return 1;
                    }

                    if (subversionArguments == null)
                    {
                        return 1;
                    }

                    projectSettings = LoadProjectSettings(subversionArguments);
                }
                catch (Exception)
                {
                    return 1;
                }
            }

            try
            {
                var form = new GollumForm(projectSettings, subversionArguments);
                Application.Run(form);
            }
            catch (Exception e)
            {
                // The error printed here shows up in the tortoiseSVN commit window.
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine("Stack trace:");
                Console.Error.WriteLine(e.StackTrace);
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Finds the project root, either the given directory or any of its parent directories.
        /// </summary>
        /// <param name="directory">Directory where to start searching</param>
        /// <param name="filename">Name of the file that should be found from root</param>
        /// <exception cref="ArgumentException">Thrown if given directory does not exist or if given filename is null or empty</exception>
        /// <exception cref="FileNotFoundException">Thrown if file is not found from given directory or any of its parent directories</exception>
        /// <returns>Path to the project root directory.</returns>
        public static string FindProjectRootDirectory(string directory, string filename)
        {
            if (!Directory.Exists(directory))
            {
                throw new ArgumentException("Given directory does not exist: " + directory);
            }
            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("Null or empty filename");
            }

            string currentDirectory = directory;
            while (!File.Exists(Path.Combine(currentDirectory, filename)))
            {
                var di = new DirectoryInfo(currentDirectory);
                if (di.Parent == null || !di.Parent.Exists)
                {
                    throw new FileNotFoundException(String.Format("File \"{0}\" not found in given directory \"{1}\" or in any of its parent directories", filename, directory));
                }

                currentDirectory = di.Parent.FullName;
            }
            return currentDirectory;
        }

        private static SubversionArguments ReadCommandLineArguments(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                ShowHelp();
                return null;
            }

            int revision;
            if (!int.TryParse(args[1], out revision))
            {
                ShowHelp();
                Console.Error.WriteLine("Revision must be integer");
                return null;
            }

            string cwd = Directory.GetCurrentDirectory();
            if (args.Length == 3)
            {
                cwd = args[2];
                if (!Directory.Exists(cwd))
                {
                    ShowHelp();
                    Console.Error.WriteLine("Cwd doesn't exist");
                    return null;
                }
            }

            var svnArgs = new SubversionArguments
            {
                RevisionTo = revision,
                Cwd = cwd,
                LocalProjectRootDirectory = FindProjectRootDirectory(cwd, ProjectSettings.DefaultFileName)
            };

            try
            {
                svnArgs.Message = SvnPatchCreator.GetMessageForRevision(svnArgs).Result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unable to get message for revision: " + ex.Message);
                return null;
            }

            return svnArgs;
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("gollum pathfile depth messagefile revision error cwd");
            Console.WriteLine("gollum -r revision [cwd]");
        }

        private static SubversionArguments ReadTortoiseArguments(IList<string> args)
        {
            if (args.Count != 6)
            {
                ShowHelp();
                return null;
            }

            string pathFile = args[0];
            int depth;
            if (!Int32.TryParse(args[1], out depth))
            {
                Console.Error.WriteLine("depth must be an integer");
                return null;
            }
            if (!((-2 <= depth) && (depth <= 3)))
            {
                Console.Error.WriteLine("depth must be between -2 and 3");
                return null;
            }

            string messageFile = args[2];
            string revision = args[3];
            string errorFile = args[4];
            string cwd = args[5];

            return new SubversionArguments
            {
                Paths = File.ReadAllLines(pathFile),
                Depth = (SvnDepth)depth,
                Message = File.ReadAllText(messageFile),
                RevisionTo = Int32.Parse(revision),
                Error = File.ReadAllText(errorFile),
                Cwd = cwd,
                LocalProjectRootDirectory = FindProjectRootDirectory(cwd, ProjectSettings.DefaultFileName)
            };
        }

        private static ProjectSettings LoadProjectSettings(SubversionArguments subversionArguments)
        {
            return ProjectSettings.Load(Path.Combine(subversionArguments.LocalProjectRootDirectory, ProjectSettings.DefaultFileName));
        }
    }
}
