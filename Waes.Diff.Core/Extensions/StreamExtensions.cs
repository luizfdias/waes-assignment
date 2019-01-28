using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Core.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Converts a stream to byte array
        /// </summary>
        /// <param name="value">The stream</param>
        /// <returns>Array of bytes</returns>
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
