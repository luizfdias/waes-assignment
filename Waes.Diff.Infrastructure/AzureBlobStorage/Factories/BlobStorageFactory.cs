using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Diagnostics.CodeAnalysis;
using Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Factories
{
    public class BlobStorageFactory : IBlobStorageFactory
    {
        public string ConnectionString { get; }

        public BlobStorageFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));
            
            ConnectionString = connectionString;
        }

        [ExcludeFromCodeCoverage]
        public CloudBlobContainer Create(string containerName)
        {
            if (!CloudStorageAccount.TryParse(ConnectionString, out var cloudStorageAccount))
                throw new Exception();

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            return cloudBlobContainer;
        }
    }
}
