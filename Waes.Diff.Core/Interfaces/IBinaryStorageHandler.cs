using System.IO;
using System.Threading.Tasks;

namespace Waes.Diff.Core.Interfaces
{
    public interface IBinaryStorageHandler
    {
        Task Save(string id, Stream stream);
    }
}
