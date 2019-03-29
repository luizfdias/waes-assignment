using System.Collections.Generic;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;

namespace Waes.Assignment.IntegrationTests.Database
{
    public class InMemoryDatabaseTest<TEntity> : IDatabase<TEntity> where TEntity : Entity
    {
        public ICollection<TEntity> Entities { get; }

        public InMemoryDatabaseTest()
        {
            Entities = new List<TEntity>();
        }

        public InMemoryDatabaseTest(ICollection<TEntity> entities)
        {
            Entities = entities;
        }
    }
}
