using System.Threading;
using System.Threading.Tasks;
using Aidon.Tools.Gollum.Bugzilla;

namespace Aidon.Tools.Gollum
{
    public class DummyBugzillaRestClient : IBugzillaHandler
    {
        public async Task<bool> PostToBugzillaAsync(BugzillaArguments arguments)
        {
            await Task.Delay(3000).ConfigureAwait(false);
            return true;
        }

        public async Task<BugzillaBug> GetBugInformationAsync(BugzillaArguments arguments, CancellationToken token)
        {
            await Task.Delay(3000, token).ConfigureAwait(false);
            return new BugzillaBug("<bug></bug>");
        }
    }
}
