namespace Aidon.Tools.Gollum.ReviewBoard
{
    /// <summary>
    /// Container class for arguments needed to create a review board review request
    /// </summary>
    public class ReviewBoardArguments
    {
        /// <summary>
        /// Review board repository name. For example: Gollum, Gateware or Meteringware
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// Review board reviewer group. Tells which users are allowed to review this ticket. For example: All, Gateware, Gollum or Meterinware
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Difference to the repository directory defined in review board. For example with Gateware: /Branches/Gateware2.8
        /// If a commit is made from a subdirectory this needs to be updated.
        /// </summary>
        public string BaseDirectory { get; set; }

        /// <summary>
        /// The path to the diff file containing the changes made in the commit. Including the filename.
        /// </summary>
        public string DiffFile { get; set; }

        /// <summary>
        /// Review board summary
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Review board description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the bugs.
        /// </summary>
        /// <value>
        /// The bugs in a comma separated list.
        /// </value>
        public string Bugs { get; set; }

        /// <summary>
        /// Callback for asking credentials from the user when needed.
        /// </summary>
        public CredentialCallback CredentialCallback { get; set; }
    }
}