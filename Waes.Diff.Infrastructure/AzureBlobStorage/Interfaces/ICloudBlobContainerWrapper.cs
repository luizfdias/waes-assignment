using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces
{
    public interface ICloudBlobContainerWrapper
    {
        Task UploadFromByteArrayAsync(string id, byte[] data);

        Task<Stream> DownloadToStreamAsync(string id);
    }
}
