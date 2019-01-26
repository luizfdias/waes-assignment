using System.IO;

namespace Waes.Diff.Core.UnitTests.Helpers
{
    public static class BinaryDataWriter
    {
        public static byte[] Write(params string[] args)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    foreach (var arg in args)
                    {
                        bw.Write(arg);
                    }
                }

                return ms.ToArray();
            }
        }
    }
}
