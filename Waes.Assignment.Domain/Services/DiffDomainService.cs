﻿using System;
using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Services
{
    public class DiffDomainService : IDiffDomainService 
    { 
        public Diff ProcessDiff<TEquatable>(TEquatable[] left, TEquatable[] right) where TEquatable : IEquatable<TEquatable>
        {
            if (left.Count() != right.Count())
                return Diff.CreateNotOfEqualSize();

            var diffs = new List<DiffPosition>();

            for (int i = 0; i < left.Length; i++)
            {
                if (!left[i].Equals(right[i]))
                {
                    diffs.Add(new DiffPosition(i));
                }
            }

            return diffs.Any() ? Diff.CreateNotEqual(diffs) : Diff.CreateEqual();
        }
    }
}