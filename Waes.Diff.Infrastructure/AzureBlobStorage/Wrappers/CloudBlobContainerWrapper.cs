using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Wrappers
{
    /*
        I created this wrapper to isolate the not testable code. Its wrapper the use of Azure Blob Storage.
    */

    /// <summary>
    /// Implementation of ICloudBlobContainerWrapper
    /// </summary>
    public class CloudBlobContainerWrapper : ICloudBlobContainerWrapper
    {
        private readonly CloudBlobContainer _container;

        public IBlobStorageFactory BlobStorageFactory { get; }

        public CloudBlobContainerWrapper(IBlobStorageFactory blobStorageFactory, string containerName)
        {
            if (string.IsNullOrWhiteSpace(containerName))
                throw new ArgumentNullException(nameof(containerName));
            
            BlobStorageFactory = blobStorageFactory ?? throw new ArgumentNullException(nameof(blobStorageFactory));

            _container = BlobStorageFactory.Create(containerName);
        }

        public async Task UploadFromByteArrayAsync(string id, byte[] data)
        {
            var blockBlobReference = _container.GetBlockBlobReference(id);

            await blockBlobReference.UploadFromByteArrayAsync(data, 0, data.Length);
        }        

        public async Task<Stream> DownloadToStreamAsync(string id)
        {            
            var blockBlobReference = _container.GetBlockBlobReference(id);

            using (var ms = new MemoryStream())
            {
                await blockBlobReference.DownloadToStreamAsync(ms);

                return ms;
            }
        }
    }
}
