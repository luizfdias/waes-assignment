using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Core.Interfaces
{
    /// <summary>
    /// DataStorageHandler contract, used to handle data.
    /// </summary>
    public interface IDataStorageHandler
    {
        /// <summary>
        /// Saves the data in a DataStorage
        /// </summary>
        /// <param name="id">The identification of data</param>
        /// <param name="stream">The stream to be handled</param>
        Task Save(string id, Stream stream);
    }
}
