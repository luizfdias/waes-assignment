using System.Collections.Generic;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models;
using Waes.Assignment.Domain.Models.Abstractions;

namespace Waes.Assignment.Domain.Services
{
    public class DiffEngine : IDiffEngine
    {
        public Diff ProcessDiff(IDifferableList left, IDifferableList right)
        {            
            var diffs = new List<DiffInfo>();

            int length = 0;
            int startIndex = 0;

            for (int i = 0; i < left.Count(); i++)
            {
                if (left[i].CompareTo(right[i]) == 0)
                {
                    if (length > 0)
                        diffs.Add(new DiffInfo(startIndex, length));

                    length = 0;
                }
                else
                {
                    if (length == 0)
                        startIndex = i;

                    length += 1;

                    if (i + 1 == left.Count())
                        diffs.Add(new DiffInfo(startIndex, length));
                }
            }

            return new Diff
            {
                Info = diffs
            };
        }
    }
}
