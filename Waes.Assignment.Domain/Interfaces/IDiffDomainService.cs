using System;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IDiffDomainService
    {
        DiffInfo ProcessDiff<TEquatable>(TEquatable[] left, TEquatable[] right) where TEquatable : IEquatable<TEquatable>;
    }
}
