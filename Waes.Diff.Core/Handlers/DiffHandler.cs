using System.Threading.Tasks;
using Waes.Diff.Core.Exceptions;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Handlers
{
    public class DiffHandler : IDiffHandler
    {
        public IBinaryDataStorage BinaryDataStorage { get; }

        public IDiffChecker DiffChecker { get; }

        public DiffHandler(IBinaryDataStorage binaryDataStorage, IDiffChecker diffChecker)
        {
            BinaryDataStorage = binaryDataStorage ?? throw new System.ArgumentNullException(nameof(binaryDataStorage));
            DiffChecker = diffChecker ?? throw new System.ArgumentNullException(nameof(diffChecker));
        }

        public async Task<DiffResult> Diff(string id)
        {
            var task1 = BinaryDataStorage.Get("left" + id);
            var task2 = BinaryDataStorage.Get("right" + id);

            await Task.WhenAll(task1, task2);

            var leftData = task1.Result ?? throw new BinaryDataNotFoundException("left" + id);
            var rightData = task2.Result ?? throw new BinaryDataNotFoundException("right" + id);

            return DiffChecker.Check(leftData, rightData);
        }
    }
}
