using System.Threading;
using System.Threading.Tasks;
using Aidon.Tools.Gollum.Bugzilla;

namespace Aidon.Tools.Gollum
{
    public interface IBugzillaHandler
    {
        Task PostToBugzillaAsync(BugzillaArguments arguments);
        Task<BugzillaBug> GetBugInformationAsync(BugzillaArguments arguments, CancellationTokenSource token);
    }
}