using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces
{
    /// <summary>
    /// An Wrapper to a CloudBlobContainer
    /// </summary>
    public interface ICloudBlobContainerWrapper
    {
        /// <summary>
        /// Uploads a byte array inside the container
        /// </summary>
        /// <param name="id">The identification of data</param>
        /// <param name="data">The data</param>
        /// <returns></returns>
        Task UploadFromByteArrayAsync(string id, byte[] data);

        /// <summary>
        /// Downloads the data to a stream.
        /// </summary>
        /// <param name="id">The identification of data</param>
        /// <returns>A stream of data</returns>
        Task<Stream> DownloadToStreamAsync(string id);
    }
}
