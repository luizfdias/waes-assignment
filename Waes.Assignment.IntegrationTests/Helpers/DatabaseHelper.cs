using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Domain.ValueObjects;
using Waes.Assignment.Infra.Repositories.InMemory;

namespace Waes.Assignment.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        public static InMemoryDatabase<PayLoad> CreatePayloads()
        {
            var payloadDatabase = new InMemoryDatabase<PayLoad>();

            payloadDatabase.Entities.Add(new PayLoad("123456789", new byte[] { 97, 98, 99, 49, 50, 51 }, SideEnum.Left));

            return payloadDatabase;
        }

        public static InMemoryDatabase<Diff> CreateDiffs()
        {
            var diffDatabase = new InMemoryDatabase<Diff>();

            var diff = new Diff("789456123", new DiffInfo(DiffStatus.Equal));

            diffDatabase.Entities.Add(diff);

            return diffDatabase;
        }
    }
}
