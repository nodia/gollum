using Aidon.Tools.Gollum.Bugzilla;

namespace Aidon.Tools.Gollum
{
    public interface IBugzillaHandler
    {
        void PostToBugzilla(BugzillaArguments arguments);
        BugzillaBug GetBugInformation(BugzillaArguments arguments);
    }
}