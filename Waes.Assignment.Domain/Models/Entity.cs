using System;

namespace Waes.Assignment.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
