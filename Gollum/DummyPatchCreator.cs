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

        public static async Task<string> GetMessageForRevision(SubversionArguments svnArguments)
        {
            await Task.Delay(2000).ConfigureAwait(false);
            return string.Format("Test commit message for revision range {0} - {1}. Fixed bug #313.", svnArguments.RevisionFrom, svnArguments.RevisionTo);
        }
    }
}