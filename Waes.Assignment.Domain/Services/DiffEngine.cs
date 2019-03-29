using System;
using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Domain.Services
{
    public class DiffEngine : IDiffEngine
    {
        private readonly IDifferenceIntervalFinder _differenceIntervalFinder;

        public DiffEngine(IDifferenceIntervalFinder differenceIntervalFinder)
        {
            _differenceIntervalFinder = differenceIntervalFinder ?? throw new ArgumentNullException(nameof(differenceIntervalFinder));
        }

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
