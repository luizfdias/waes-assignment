using System.Threading.Tasks;

namespace Waes.Diff.Core.Interfaces
{
    public interface IBinaryDataStorage
    {
        Task Save(string id, byte[] data);

        Task<byte[]> Get(string id);
    }
}
