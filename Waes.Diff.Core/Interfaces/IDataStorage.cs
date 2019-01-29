using System.Threading.Tasks;

namespace Waes.Diff.Core.Interfaces
{
    /*
        I chose to create the interface that is implemented by the repositories here, in the Core layer. 
        In that way, the domain doesn't depend of the Infrastructure layer, which make easier to change non business components as the data storage and etc.
    */

    /// <summary>
    /// DataStorage contract, used to handle data stored.
    /// </summary>
    public interface IDataStorage
    {
        /// <summary>
        /// Saves the data in a DataStorage
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
