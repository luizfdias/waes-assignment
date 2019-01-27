using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Core.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ConvertToByteArrayAsync(this Stream value)
        {
            using (var ms = new MemoryStream())
            {
                await value.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
