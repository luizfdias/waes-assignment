using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Interfaces
{
    public interface IDiffChecker
    {
        DiffResult Check(byte[] leftBuffer, byte[] rightBuffer);
    }
}
