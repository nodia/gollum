using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Aidon.Tools.Gollum.SVN
{
    public class SvnPatchCreator : IPatchCreator
    {

        /// <summary>
        /// Creates the patch.
        /// </summary>
        /// <param name="svnArguments">The SVN arguments.</param>
        /// <returns>
        /// A task that represents the asynchronous patch operation. 
        /// The value of the Result property contains the file path of created patch file.
        /// </returns>
        /// <exception cref="System.Exception">
        /// Thrown when revision from is greater than or equal to revision to
        /// or
        /// Failed to start SVN process
        /// or
        /// An error occurred in SVN process
        /// </exception>
        public async Task<string> CreatePatchAsync(SubversionArguments svnArguments)
        {
            string patchFilePath = Path.GetTempFileName().Replace(".tmp", ".patch");

            if (svnArguments.RevisionFrom >= svnArguments.RevisionTo)
            {
                throw new Exception("SVN error: RevisionFrom must be less than RevisionTo!");
            }

            string arguments = String.Format("diff -r {0}:{1}", svnArguments.RevisionFrom,  svnArguments.RevisionTo);
            
            try
            {
                string output = await StartAndGetOutputFromProcess("svn", arguments, svnArguments.Cwd).ConfigureAwait(false);

                // Save output to file
                using (TextWriter tw = new StreamWriter(patchFilePath, false, Encoding.UTF8))
                {
                    tw.Write(output);
                }

                return patchFilePath;
            }
            catch (Win32Exception ex)
            {
                File.Delete(patchFilePath);
                throw new Exception("Failed to start SVN process: " + ex.Message + ". Error code: " + ex.NativeErrorCode, ex);
            }
            catch (Exception ex)
            {
                File.Delete(patchFilePath);
                throw new Exception("An error occurred SVN process: " + ex.Message, ex);
            }
        }

        public static async Task<string> GetMessageForRevision(SubversionArguments svnArguments)
        {
            string arguments = String.Format("log -r{0} --xml", svnArguments.RevisionTo);

            string output = await StartAndGetOutputFromProcess("svn", arguments, svnArguments.Cwd).ConfigureAwait(false);

            // XML, because of enterprise!
            var xml = new XmlDocument();
            xml.LoadXml(output);
            var node = xml.SelectSingleNode("log/logentry/msg");
            if (node == null || node.FirstChild == null || node.FirstChild.Value == null)
            {
                throw new Exception("SVN error: No such revision!");
            }

            return node.FirstChild.Value;

        }

        private static async Task<string> StartAndGetOutputFromProcess(string processName, string arguments, string workingDirectory)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = processName,
                    Arguments = arguments,
                    WorkingDirectory = workingDirectory,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    RedirectStandardOutput = true
                }
            };

            if (!process.Start())
            {
                throw new InvalidOperationException("Failed to start '" + processName + "' process.");
            }

            string output = await process.StandardOutput.ReadToEndAsync().ConfigureAwait(false);

            process.WaitForExit(60000);

            if (process.ExitCode != 0)
            {
                throw new Win32Exception(process.ExitCode, "The process '" + processName + "' exited with exit code " + process.ExitCode);
            }
            if (String.IsNullOrWhiteSpace(output))
            {
                throw new InvalidOperationException("The process '" + processName + "' output was null or empty.");
            }
            return output;
        }
    }
}
