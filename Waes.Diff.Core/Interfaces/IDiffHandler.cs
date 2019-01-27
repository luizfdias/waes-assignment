using System.Threading.Tasks;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Interfaces
{
    public interface IDiffHandler
    {
        Task<DiffResult> Diff(string id);
    }
}
