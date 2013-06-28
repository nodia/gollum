using System.Threading;
using System.Threading.Tasks;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum
{
    class DummyPatchCreator : IPatchCreator
    {
        public async Task<string> CreatePatchAsync(SubversionArguments arguments)
        {
            await Task.Delay(3000).ConfigureAwait(false);
            return @"C:\test.diff";
        }
    }
}