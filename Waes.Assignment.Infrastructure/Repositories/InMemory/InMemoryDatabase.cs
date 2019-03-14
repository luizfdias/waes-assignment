using System.Collections.Generic;
using Waes.Assignment.Domain.Interfaces;

namespace Waes.Assignment.Infrastructure.Repositories.InMemory
{
    public class InMemoryDatabase<TEntity> where TEntity : Entity
    {
        public List<TEntity> Entities { get; }

        public InMemoryDatabase()
        {
            Entities = new List<TEntity>();
        }
    }
}
