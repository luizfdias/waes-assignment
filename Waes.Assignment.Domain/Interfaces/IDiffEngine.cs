using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Abstractions;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IDiffEngine
    {
        Diff ProcessDiff(IDifferableList left, IDifferableList right);
    }
}
