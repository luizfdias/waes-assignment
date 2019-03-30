using System;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// An abstraction of the repository pattern
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Gets the entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(Guid id);

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task Add(TEntity item);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task Remove(TEntity item);        

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task Update(TEntity item);
    }
}
