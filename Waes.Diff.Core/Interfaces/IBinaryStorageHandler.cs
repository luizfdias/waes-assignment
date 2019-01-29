using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Core.Interfaces
{
    /// <summary>
    /// BinaryStorageHandler contract, used to handle binary data.
    /// </summary>
    public interface IBinaryStorageHandler
    {
        /// <summary>
        /// Saves the data in a BinaryDataStorage
        /// </summary>
        /// <param name="id">The identification of data</param>
        /// <param name="stream">The stream to be handled</param>
        Task Save(string id, Stream stream);
    }
}
