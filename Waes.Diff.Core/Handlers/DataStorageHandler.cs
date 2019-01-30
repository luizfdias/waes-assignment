using System;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Handlers
{
    /// <summary>
    /// DataStorageHandler implementation
    /// </summary>
    public class DataStorageHandler : IDataStorageHandler
    {
        public IDataStorage DataStorage { get; }

        public DataStorageHandler(IDataStorage dataStorage)
        {
            DataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public async Task Save(Data data)
        {
            await DataStorage.Save(data);
        }
    }
}
