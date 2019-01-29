﻿using System;
using System.IO;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Repositories
{
    /*
        Besides the use of in memory cache, I developed an alternative to it in case of be necessary to persist data for futures comparisons.
        Blob storage for Azure is a good choice for store data. A improvement in this part could be to store metadata as for example 
        the size of the data.
    */

    /// <summary>
    /// DataStorage implementation using Azure Blob Storage
    /// </summary>
    public class BlobStorageRepository : IDataStorage
    {
        public ICloudBlobContainerWrapper CloudBlobContainerWrapper { get; }

        public BlobStorageRepository(ICloudBlobContainerWrapper cloudBlobContainerWrapper)
        {
            CloudBlobContainerWrapper = cloudBlobContainerWrapper ?? throw new ArgumentNullException(nameof(cloudBlobContainerWrapper));
        }        

        public async Task<byte[]> Get(string id)
        {
            var stream = await CloudBlobContainerWrapper.DownloadToStreamAsync(id);

            return stream != null ? ((MemoryStream)stream).ToArray() : null;
        }

        public async Task Save(string id, byte[] data)
        {
            await CloudBlobContainerWrapper.UploadFromByteArrayAsync(id, data);
        }
    }
}
