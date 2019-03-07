using System;

namespace Waes.Assignment.Domain.Models.Abstractions
{
    public interface IDifferableList : IIndexable
    {
        int Count();
    }
}
