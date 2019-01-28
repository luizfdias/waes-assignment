using System.Threading.Tasks;

namespace Waes.Diff.Core.Interfaces
{
    /// <summary>
    /// BinaryDataStorage contract, used to handle binary data stored.
    /// </summary>
    public interface IBinaryDataStorage
    {
        /// <summary>
        /// Saves the data in a BinaryDataStorage
        /// </summary>
        /// <param name="id">The identification of data</param>
        /// <param name="data">The data to be saved</param>
        Task Save(string id, byte[] data);


        /// <summary>
        /// Retrieves the data stored
        /// </summary>
        /// <param name="id">The identification of data</param>
        /// <returns>The data</returns>
        Task<byte[]> Get(string id);
    }
}
