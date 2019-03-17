﻿using System;
using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Models
{
    public class Diff : Entity
    {
        public IEnumerable<DiffPosition> DiffPositions { get; private set; }

        public DiffStatus Status { get; }

        public string CorrelationId { get; set; }

        public Diff(Guid id, DiffStatus status)
        {
            Id = id;
            Status = status;
        }

        public IEnumerable<DiffSequence> GetSequenceOfDifferences()
        {
            var sequences = new List<DiffSequence>();

            var array = DiffPositions.ToArray();

            int currentLen = 1, currentIdx = 0;

            for (int i = 0; i < array.Length; i++)
            {
                int nextIndex = i + 1;

                if (nextIndex < array.Length && array[i].Position + 1 == array[nextIndex].Position)
                {
                    currentLen++;                                       
                }
                else
                {
                    sequences.Add(new DiffSequence(currentIdx, currentLen));
                    currentIdx = nextIndex;

                    currentLen = 1;
                }
            }

            return sequences;
        }

        #region Factories
        public static Diff CreateEqual()
        {
            return new Diff(Guid.NewGuid(), DiffStatus.Equal);
        }

        public static Diff CreateNotOfEqualSize()
        {
            return new Diff(Guid.NewGuid(), DiffStatus.NotOfEqualSize);
        }

        public static Diff CreateNotEqual(IEnumerable<DiffPosition> diffPositions)
        {
            return new Diff(Guid.NewGuid(), DiffStatus.NotEqual)
            {
                DiffPositions = diffPositions
            };
        }
        #endregion
    }
}