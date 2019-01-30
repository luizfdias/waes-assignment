using System.Threading.Tasks;
using Waes.Diff.Core.Models;

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
        /// <param name="data">The data</param>        
        Task Save(Data data);
    }
}
