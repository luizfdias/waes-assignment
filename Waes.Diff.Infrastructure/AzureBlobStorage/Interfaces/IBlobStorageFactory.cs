using Microsoft.WindowsAzure.Storage.Blob;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces
{
    public interface IBlobStorageFactory
    {
        CloudBlobContainer Create(string containerName);
    }
}
