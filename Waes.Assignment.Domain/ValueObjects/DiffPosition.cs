using System;

namespace Waes.Assignment.Domain.ValueObjects
{
    public class DiffPosition
    {
        public int Position { get; }

        public DiffPosition(int position)
        {
            if (position < 0)
                throw new ArgumentException("Position should not be less than zero", nameof(position));

            Position = position;
        }
    }
}
