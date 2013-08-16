using System;
using System.Threading;
using System.Threading.Tasks;
using Aidon.Tools.Gollum.Bugzilla;

namespace Aidon.Tools.Gollum
{
    public class DummyBugzillaRestClient : IBugzillaHandler
    {
        public async Task PostToBugzillaAsync(BugzillaArguments arguments)
        {
            await Task.Delay(3000).ConfigureAwait(false);
        }

        public async Task<BugzillaBug> GetBugInformationAsync(BugzillaArguments arguments, CancellationTokenSource tokenSource)
        {
            tokenSource.CancelAfter(TimeSpan.FromSeconds(4));
            await Task.Delay(3000, tokenSource.Token).ConfigureAwait(false);
            return new BugzillaBug("<bug></bug>");
        }
    }
}
