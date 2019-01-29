using System;
using System.IO;
using System.Threading.Tasks;
using Waes.Diff.Core.Extensions;
using Waes.Diff.Core.Interfaces;

namespace Waes.Diff.Core.Handlers
{
    /// <summary>
    /// BinaryStorageHandler implementation
    /// </summary>
    public class BinaryStorageHandler : IBinaryStorageHandler
    {
        public IBinaryDataStorage BinaryDataStorage { get; }

        public BinaryStorageHandler(IBinaryDataStorage binaryDataStorage)
        {
            BinaryDataStorage = binaryDataStorage ?? throw new ArgumentNullException(nameof(binaryDataStorage));
        }

        public async Task Save(string id, Stream stream)
        {
            var result = await stream.ConvertToByteArrayAsync();

            await BinaryDataStorage.Save(id, result);
        }
    }
}
