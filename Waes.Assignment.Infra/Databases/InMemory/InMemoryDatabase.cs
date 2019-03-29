using System.Collections.Generic;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;

namespace Waes.Assignment.Infra.Repositories.Databases.InMemory
{
    public class InMemoryDatabase<TEntity> : IDatabase<TEntity> where TEntity : Entity
    {
        public ICollection<TEntity> Entities { get; }

        public InMemoryDatabase()
        {
            Entities = new List<TEntity>();
        }
    }
}
