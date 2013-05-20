namespace Aidon.Tools.Gollum.Bugzilla
{
    public class BugzillaArguments
    {
        public string UpdateToken { get; set; }
        public string BugId { get; set; }
        public string Comment { get; set; }
        public string Resolution { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// Callback for asking credentials from the user when needed.
        /// </summary>
        public CredentialCallback CredentialCallback { get; set; }
    }
}
