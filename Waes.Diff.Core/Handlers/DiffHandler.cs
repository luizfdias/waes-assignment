using System.Linq;
using System.Threading.Tasks;
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

        public async Task<DiffResult> Diff(string correlationId)
        {
            var data = await DataStorage.GetByCorrelationId(correlationId);
                        
            var result = DiffChecker.Check(
                data.FirstOrDefault(x => x.Side == SideEnum.Left), 
                data.FirstOrDefault(x => x.Side == SideEnum.Right));

            result.Data = data;

            return result;
        }
    }
}
