using System;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IDiffEngine
    {
        Diff ProcessDiff<TEquatable>(string correlationId, TEquatable[] left, TEquatable[] right) where TEquatable : IEquatable<TEquatable>;
    }
}
