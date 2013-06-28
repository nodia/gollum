using System;

namespace Aidon.Tools.Gollum.SVN
{
    public enum SvnDepth
    {
        Unknown = -2,
        Exclude = -1,
        Empty = 0,
        Files = 1,
        Immediates = 2,
        Infinity = 3
    }

    /// <summary>
    /// Arguments from TortoiseSVN.
    /// </summary>
    public class SubversionArguments
    {
        /// <summary>
        /// Paths of the committed files.
        /// </summary>
        public string[] Paths { get; set; }

        /// <summary>
        /// The depth with which the commit is done.
        /// </summary>
        public SvnDepth Depth { get; set; }

        /// <summary>
        /// The commit message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Svn revision after the commit.
        /// </summary>
        public int RevisionTo { get; set; }

        /// <summary>
        /// An error text or an empty string if there were no errors from subversion.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// The directory where the script is run.
        /// </summary>
        public string Cwd { get; set; }

        /// <summary>
        /// The project root directory, which should contain the settings file.
        /// </summary>
        public string LocalProjectRootDirectory { get; set; }

        private int _revisionFrom = -1;

        /// <summary>
        /// Gets or sets the revision to diff from. By default, use second-to-last revision.
        /// </summary>
        public int RevisionFrom
        {
            get
            {
                if (_revisionFrom == -1)
                {
                    return RevisionTo - 1;
                }
                return _revisionFrom;
            }
            set { _revisionFrom = value; }
        }
    }
}
