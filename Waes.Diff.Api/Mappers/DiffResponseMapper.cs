using System.Collections.Generic;
using System.Linq;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Api.Mappers
{
    public class DiffResponseMapper
    {
        public DiffResponse Map(DiffResult diffResult)
        {
            var diffResponse = new DiffResponse
            {
                EqualsSize = diffResult.SameSize
            };

            diffResponse.Differences = diffResult.Diffs.Select(x => new Difference(x.StartOffSet, x.Length));
        }
    }
}
