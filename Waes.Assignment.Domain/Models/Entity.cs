using System;

namespace Waes.Assignment.Domain.Models
{
    /// <summary>
    /// Abstraction for the entities
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// The identification
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Base constructor of <see cref="Entity"/> with id
        /// </summary>
        /// <param name="id"></param>
        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
