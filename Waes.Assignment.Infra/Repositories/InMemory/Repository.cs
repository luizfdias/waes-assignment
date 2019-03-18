using System;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Interfaces;

namespace Waes.Assignment.Infra.Repositories.InMemory
{   
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected InMemoryDatabase<TEntity> Database { get; }

        public Repository(InMemoryDatabase<TEntity> database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task Add(TEntity item)
        {
            Database.Entities.Add(item);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return Database.Entities.FirstOrDefault(x => x.Id == id);
        }

        public async Task Remove(TEntity item)
        {
            Database.Entities.Remove(item);
        }

        public async Task Update(TEntity item)
        {
            Database.Entities.Remove(item);

            Database.Entities.Add(item);
        }
    }
}
