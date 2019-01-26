using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Core.Helpers
{
    public class Base64Converter
    {
        public async Task<byte[]> Convert(Stream stream)
        {
            using (var ms = new MemoryStream(2048))
            {
                await stream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
