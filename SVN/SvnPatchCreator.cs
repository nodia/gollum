using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Aidon.Tools.Gollum.SVN
{
    public class SvnPatchCreator : IPatchCreator
    {
        /// <summary>
        /// Creates a patch file for given revision (between it and a one before it)
        /// </summary>
        /// <param name="svnArguments">SVN arguments</param>
        /// <returns>File path of created patch file</returns>
        public string CreatePatch(SubversionArguments svnArguments)
        {
            string patchFilePath = Path.GetTempFileName().Replace(".tmp", ".patch");

            if (svnArguments.RevisionFrom >= svnArguments.RevisionTo)
            {
                throw new Exception("SVN error: RevisionFrom must be less than RevisionTo!");
            }

            string arguments = String.Format("diff -r {0}:{1}", svnArguments.RevisionFrom,  svnArguments.RevisionTo);
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "svn",
                    Arguments = arguments,
                    WorkingDirectory = svnArguments.Cwd,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.UTF8,
                }
            };
            
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            // Save output to file
            using (TextWriter tw = new StreamWriter(patchFilePath, false, Encoding.UTF8))
            {
                tw.Write(process.StandardOutput.ReadToEnd());
            }
            process.WaitForExit();

            if (process.ExitCode != 0 || new FileInfo(patchFilePath).Length == 0)
            {
                // Destroy created file
                File.Delete(patchFilePath);

                throw new Exception("SVN error: Creation of the patch file failed!");
            }
            return patchFilePath;
        }

        public static string GetMessageForRevision(SubversionArguments svnArguments)
        {
            string arguments = String.Format("log -r{0} --xml", svnArguments.RevisionTo);
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "svn",
                    Arguments = arguments,
                    WorkingDirectory = svnArguments.Cwd,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.UTF8,
                }
            };

            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0 || string.IsNullOrWhiteSpace(output))
            {
                throw new Exception("SVN error: Reading the commit message failed!");
            }

            // XML, because of enterprise!
            var xml = new XmlDocument();
            xml.LoadXml(output);
            var node = xml.SelectSingleNode("log/logentry/msg");
            if (node == null)
            {
                throw new Exception("SVN error: No such revision!");
            }

            return node.FirstChild.Value;
        }
    }
}
