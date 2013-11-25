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
        /// <exception cref="ArgumentException">
        /// Thrown when revision from is greater than or equal to revision to
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Failed to start SVN process or could not get process output.
        /// </exception>
        public async Task<string> CreatePatchAsync(SubversionArguments svnArguments)
        {
            string patchFilePath = Path.GetTempFileName().Replace(".tmp", ".patch");

            if (svnArguments.RevisionFrom >= svnArguments.RevisionTo)
            {
                throw new ArgumentException("SVN error: RevisionFrom must be less than RevisionTo!");
            }

            string arguments = String.Format("diff -r {0}:{1}", svnArguments.RevisionFrom,  svnArguments.RevisionTo);
            
            try
            {
                string output = await GetProcessOutputAsync("svn", arguments, svnArguments.Cwd).ConfigureAwait(false);

                // Save output to file
                using (TextWriter tw = new StreamWriter(patchFilePath, false, Encoding.UTF8))
                {
                    tw.Write(output);
                }

                return patchFilePath;
            }
            catch (InvalidOperationException)
            {
                File.Delete(patchFilePath);
                throw;
            }
            catch (Exception ex)
            {
                File.Delete(patchFilePath);
                throw new InvalidOperationException("An error occurred SVN process: " + ex.Message, ex);
            }
        }

        public static async Task<string> GetMessageForRevision(SubversionArguments svnArguments)
        {
            string arguments = String.Format("log -r{0} --xml", svnArguments.RevisionTo);

            string output = await GetProcessOutputAsync("svn", arguments, svnArguments.Cwd).ConfigureAwait(false);

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

        private static Task<string> GetProcessOutputAsync(string processName, string arguments, string workingDirectory)
        {
            return Task.Run(() =>
            {
                try
                {
                    var process = Process.Start(
                        new ProcessStartInfo
                        {

                            FileName = processName,
                            Arguments = arguments,
                            WorkingDirectory = workingDirectory,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            StandardOutputEncoding = Encoding.UTF8,
                            RedirectStandardOutput = true
                        });

                    string output = process.StandardOutput.ReadToEnd();

                    if (!process.WaitForExit(5000))
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to kill process '{0}': {1}", processName, ex);
                        }
                    }

                    if (String.IsNullOrWhiteSpace(output))
                    {
                        throw new InvalidOperationException("The output of process '" + processName +
                                                            "' was null or empty.");
                    }

                    return output;
                }
                catch (FileNotFoundException ex)
                {
                    throw new InvalidOperationException("The file '" + ex.FileName + "' could not be found.");
                }
                catch (Win32Exception ex)
                {
                    throw new InvalidOperationException(
                        "Failed to start '" + processName + "' process: " + ex.Message + ", error code: " +
                        ex.NativeErrorCode, ex);
                }
                catch (IOException ex)
                {
                    throw new InvalidOperationException(
                        "Failed to read output from '" + processName + "' process: " + ex.Message, ex);
                }
            });
        }
    }
}
