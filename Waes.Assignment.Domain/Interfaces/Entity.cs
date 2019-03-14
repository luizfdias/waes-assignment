using System;

namespace Waes.Assignment.Domain.Interfaces
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var value = obj as Entity;

            if (value == null)
                return false;

            return value.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
