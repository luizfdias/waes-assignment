using System;
using System.Linq;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;

namespace Waes.Assignment.Infra.Repositories
{
    /// <summary>
    /// Base <see cref="Repository{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected IDatabase<TEntity> Database { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Repository{TEntity}"/> with <see cref="IDatabase{TEntity}"/>
        /// </summary>
        /// <param name="database"></param>
        public Repository(IDatabase<TEntity> database)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Add(TEntity item)
        {
            Database.Entities.Add(item);
        }

        /// <summary>
        /// Gets the entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetById(Guid id)
        {
            return Database.Entities.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Remove(TEntity item)
        {
            Database.Entities.Remove(item);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Update(TEntity item)
        {
            Database.Entities.Remove(item);

            Database.Entities.Add(item);
        }
    }
}
