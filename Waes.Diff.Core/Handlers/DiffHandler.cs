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
        public IDataStorage DataStorage { get; }

        public IDiffChecker DiffChecker { get; }

        public DiffHandler(IDataStorage dataStorage, IDiffChecker diffChecker)
        {
            DataStorage = dataStorage ?? throw new System.ArgumentNullException(nameof(dataStorage));
            DiffChecker = diffChecker ?? throw new System.ArgumentNullException(nameof(diffChecker));
        }

        public async Task<DiffResult> Diff(string id)
        {
            var leftId = $"left_{id}";
            var rightId = $"right_{id}";

            var task1 = DataStorage.Get(leftId);
            var task2 = DataStorage.Get(rightId);

            await Task.WhenAll(task1, task2);

            var leftData = task1.Result ?? throw new DataNotFoundException(leftId);
            var rightData = task2.Result ?? throw new DataNotFoundException(rightId);

            var result = DiffChecker.Check(leftData, rightData);

            result.LeftDataInfo = new DataInfo(leftId, leftData.Length);
            result.RightDataInfo = new DataInfo(rightId, rightData.Length);

            return result;
        }
    }
}
