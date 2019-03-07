using System;

namespace Waes.Assignment.Domain.Models.Abstractions
{
    public interface IIndexable
    {
        IComparable this[int index] { get; }
    }
}
