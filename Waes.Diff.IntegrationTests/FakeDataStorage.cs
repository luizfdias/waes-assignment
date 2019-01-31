using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.Diff.Core.Factories;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;
using Waes.Diff.Infrastructure.MongoDBStorage.Exceptions;

namespace Waes.Diff.IntegrationTests
{
    public class FakeDataStorage : IDataStorage
    {
        private ICollection<Data> _data;

        public FakeDataStorage()
        {
            _data = new List<Data>()
            {
                DataFactory.Create(new byte[] { 1, 2, 3 }, "Equals", Core.Models.SideEnum.Left),
                DataFactory.Create(new byte[] { 1, 2, 3 }, "Equals", Core.Models.SideEnum.Right),
                DataFactory.Create(new byte[] { 1, 2, 3 }, "NotEquals", Core.Models.SideEnum.Left),
                DataFactory.Create(new byte[] { 1, 3, 1 }, "NotEquals", Core.Models.SideEnum.Right),
                DataFactory.Create(new byte[] { 1, 2, 3, 4 }, "NotOfEqualSize", Core.Models.SideEnum.Left),
                DataFactory.Create(new byte[] { 1, 3, 1 }, "NotOfEqualSize", Core.Models.SideEnum.Right)
            };
        }

        public async Task<IEnumerable<Data>> GetByCorrelationId(string correlationId)
        {
            return _data.Where(x => x.CorrelationId == correlationId);
        }

        public async Task Save(Data data)
        {
            var dataFromMemory = await GetByCorrelationId(data.CorrelationId);

            if (dataFromMemory.Any(x => x.Side == data.Side))
                throw new CorrelationIdAlreadyUsedForDataException(data.CorrelationId, data.Side);

            _data.Add(data);
        }
    }
}
