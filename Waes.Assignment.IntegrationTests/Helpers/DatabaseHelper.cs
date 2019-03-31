using System.Collections.Generic;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        public static List<PayLoad> CreatePayloads()
        {
            var entities = new List<PayLoad>();
            entities.Add(new PayLoad("123456789", new byte[] { 97, 98, 99, 49, 50, 51 }, SideEnum.Left));

            return entities;
        }

        public static List<Diff> CreateDiffs()
        {
            var entities = new List<Diff>();

            var diff = new EqualDiff("789456123");

            entities.Add(diff);

            return entities;
        }
    }
}
