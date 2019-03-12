using System;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IDiffDomainService<TEquatable> where TEquatable : IEquatable<TEquatable>
    {
        Diff ProcessDiff(TEquatable[] left, TEquatable[] right);
    }
}
