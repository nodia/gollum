using System.Threading.Tasks;
using Aidon.Tools.Gollum.SVN;

namespace Aidon.Tools.Gollum
{
    public interface IPatchCreator
    {
        /// <summary>
        /// Creates a patch file for given revision (between it and a one before it)
        /// </summary>
        /// <param name="arguments">The SVN arguments.</param>
        /// <returns>
        /// A task that represents the asynchronous patch operation. 
        /// The value of the Result property contains the file path of created patch file.
        /// </returns>
        Task<string> CreatePatchAsync(SubversionArguments arguments);
    }
}