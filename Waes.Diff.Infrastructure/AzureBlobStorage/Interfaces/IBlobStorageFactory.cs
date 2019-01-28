using Microsoft.WindowsAzure.Storage.Blob;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces
{
    /// <summary>
    /// Factory contract used to create a new CloudBlobContainer
    /// </summary>
    public interface IBlobStorageFactory
    {
        /// <summary>
        /// Creates a new CloudBlobContainer
        /// </summary>
        /// <param name="containerName">The name of the container</param>
        /// <returns>A CloudBlobContainer</returns>
        CloudBlobContainer Create(string containerName);
    }
}
