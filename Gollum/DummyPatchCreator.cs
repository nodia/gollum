using System.Threading;
using System.Threading.Tasks;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum
{
    class DummyPatchCreator : IPatchCreator
    {
        public Task<string> CreatePatchAsync(SubversionArguments arguments)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(3000);
                return @"C:\test.diff";
            });
        }

        public static Task<string> GetMessageForRevision(SubversionArguments svnArguments)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(3000);
                return string.Format("Test commit message for revision range {0} - {1}. Fixed bug #313.",
                                     svnArguments.RevisionFrom, svnArguments.RevisionTo);
            });
        }
    }
}