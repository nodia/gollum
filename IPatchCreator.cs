using System;
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
        /// File path of created patch file
        /// </returns>
        string CreatePatch(SubversionArguments arguments);
    }
}