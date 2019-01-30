using System.Collections.Generic;
using System.Threading.Tasks;
using Waes.Diff.Core.Models;

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
        /// <param name="data">The data</param>
        Task Save(Data data);


        /// <summary>
        /// Retrieves the data stored
        /// </summary>
        /// <param name="correlationId">The correlation identification of data</param>
        /// <returns>An IEnumerable of data</returns>
        Task<IEnumerable<Data>> GetByCorrelationId(string correlationId);
    }
}
