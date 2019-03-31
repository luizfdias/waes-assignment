using System;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Domain.Interfaces
{
    /// <summary>
    /// Exposes the contract of any form of comparsion between to arrays of equatables
    /// </summary>
    public interface IDiffEngine
    {
        /// <summary>
        /// Processes the diff between two arrays of equatables
        /// </summary>
        /// <typeparam name="TEquatable"></typeparam>
        /// <param name="correlationId"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        Diff ProcessDiff<TEquatable>(string correlationId, TEquatable[] left, TEquatable[] right) where TEquatable : IEquatable<TEquatable>;
    }
}
