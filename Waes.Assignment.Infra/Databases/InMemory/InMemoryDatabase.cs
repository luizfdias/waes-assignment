using System.Collections.Generic;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Infra.Interfaces;

namespace Waes.Assignment.Infra.Repositories.Databases.InMemory
{
    /// <summary>
    /// Implementation of an in memory database with collections
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InMemoryDatabase<TEntity> : IDatabase<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// The entities collection
        /// </summary>
        public ICollection<TEntity> Entities { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="InMemoryDatabase{TEntity}"/>
        /// </summary>
        public InMemoryDatabase()
        {
            Entities = new List<TEntity>();
        }
    }
}
