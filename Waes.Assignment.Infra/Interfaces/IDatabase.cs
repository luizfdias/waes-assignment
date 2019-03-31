using System.Collections.Generic;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Infra.Interfaces
{
    /// <summary>
    /// Exposes the collection of entities in database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDatabase<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// The entities collection
        /// </summary>
        ICollection<TEntity> Entities { get; }
    }
}
