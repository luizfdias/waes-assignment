using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Waes.Diff.Core.Extensions;
using Waes.Diff.Core.Interfaces;

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

        public async Task Save(string id, Stream stream)
        {
            var result = await stream.ConvertToByteArrayAsync();

            var text = Encoding.UTF8.GetString(result);

            if (text.IsBase64String())
                await DataStorage.Save(id, Convert.FromBase64String(text));
            else
                await DataStorage.Save(id, result);
        }
    }
}
