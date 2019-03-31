using System;
using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Domain.Services
{
    /// <summary>
    /// It is responsible for process the differences between two arrays of equatables
    /// </summary>
    public class DiffEngine : IDiffEngine
    {
        private readonly IDifferenceIntervalFinder _differenceIntervalFinder;

        /// <summary>
        /// Initializes a new instance of <see cref="DiffEngine"/> with an instance of <see cref="IDifferenceIntervalFinder"/> 
        /// </summary>
        /// <param name="differenceIntervalFinder"></param>
        public DiffEngine(IDifferenceIntervalFinder differenceIntervalFinder)
        {
            _differenceIntervalFinder = differenceIntervalFinder ?? throw new ArgumentNullException(nameof(differenceIntervalFinder));
        }

        /// <summary>
        /// It processes the diff. When left and right data are not of equal size, it returns <see cref="NotOfEqualSizeDiff"/>. 
        /// When both are equal, it returns <see cref="EqualDiff"/>.
        /// When they are not equal, it returns <see cref="NotEqualDiff"/> 
        /// </summary>
        /// <typeparam name="TEquatable"></typeparam>
        /// <param name="correlationId"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public Diff ProcessDiff<TEquatable>(string correlationId, TEquatable[] left, TEquatable[] right) where TEquatable : IEquatable<TEquatable>
        {
            if (left.Count() != right.Count())
                return new NotOfEqualSizeDiff(correlationId);

            var indexOfDifferences = new List<int>();

            for (int i = 0; i < left.Length; i++)
            {
                if (!left[i].Equals(right[i]))
                {
                    indexOfDifferences.Add(i);
                }
            }

            if (indexOfDifferences.Any())
            {
                return new NotEqualDiff(correlationId, _differenceIntervalFinder.Find(indexOfDifferences.ToArray()));
            }

            return new EqualDiff(correlationId);
        }
    }
}
