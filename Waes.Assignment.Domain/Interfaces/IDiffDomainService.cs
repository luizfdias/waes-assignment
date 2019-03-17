using System;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IDiffDomainService
    {
        Diff ProcessDiff<TEquatable>(TEquatable[] left, TEquatable[] right) where TEquatable : IEquatable<TEquatable>;
    }
}
