using System.Threading.Tasks;
using Waes.Diff.Core.Exceptions;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Handlers
{
    /// <summary>
    /// DiffHandler implementation 
    /// </summary>
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
            var leftId = $"left_{id}";
            var rightId = $"right_{id}";

            var task1 = BinaryDataStorage.Get(leftId);
            var task2 = BinaryDataStorage.Get(rightId);

            await Task.WhenAll(task1, task2);

            var leftData = task1.Result ?? throw new BinaryDataNotFoundException(leftId);
            var rightData = task2.Result ?? throw new BinaryDataNotFoundException(rightId);

            var result = DiffChecker.Check(leftData, rightData);

            result.LeftDataInfo = new DataInfo(leftId, leftData.Length);
            result.RightDataInfo = new DataInfo(rightId, rightData.Length);

            return result;
        }
    }
}
