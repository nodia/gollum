using System;

namespace Aidon.Tools.Gollum.Bugzilla
{
    [Serializable]
    public class BugzillaException : Exception
    {
        public BugzillaException(string message) : base(message) { }
        public BugzillaException(string message, Exception innerException) : base(message, innerException) { }
    }
}