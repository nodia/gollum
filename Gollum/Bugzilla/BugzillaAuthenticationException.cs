using System;

namespace Aidon.Tools.Gollum.Bugzilla
{
    [Serializable]
    public class BugzillaAuthenticationException : Exception
    {
        public BugzillaAuthenticationException()
        {
        }

        public BugzillaAuthenticationException(string message)
                : base(message)
        {
        }
    }

    [Serializable]
    public class BugzillaInvalidLoginCredentialsException : BugzillaAuthenticationException
    {
        public BugzillaInvalidLoginCredentialsException(string message)
            : base(message)
        {
        }
    }
}