using System;
using System.IO;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Repositories
{
    public class BlobStorageRepository : IBinaryDataStorage
    {
        public ICloudBlobContainerWrapper CloudBlobContainerWrapper { get; }

        public BlobStorageRepository(ICloudBlobContainerWrapper cloudBlobContainerWrapper)
        {
            CloudBlobContainerWrapper = cloudBlobContainerWrapper ?? throw new ArgumentNullException(nameof(cloudBlobContainerWrapper));
        }        

        public async Task<byte[]> Get(string id)
        {
            var stream = await CloudBlobContainerWrapper.DownloadToStreamAsync(id);

            return ((MemoryStream)stream).ToArray();
        }

        public async Task Save(string id, byte[] data)
        {
            await CloudBlobContainerWrapper.UploadFromByteArrayAsync(id, data);
        }
    }
}
