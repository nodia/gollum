using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum
{
    class DummyPatchCreator : IPatchCreator
    {
        public string CreatePatch(SubversionArguments arguments)
        {
            return @"C:\Users\vlaakso\AppData\Local\Temp\testi.diff";
        }
    }
}