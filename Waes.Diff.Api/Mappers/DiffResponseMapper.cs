using System.Collections.Generic;
using System.Linq;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Core.Models;
using DataInfo = Waes.Diff.Api.Contracts.DataInfo;
using Difference = Waes.Diff.Api.Contracts.Difference;

namespace Waes.Diff.Api.Mappers
{
    /*
        I chose to have two models. One used by the application domain and another one as the contract of the api.
        The idea is that a change in the domain does not necessarily have to result in a change of the contract and vice versa.
    */

    /// <summary>
    /// DiffResponseMapper implementation
    /// </summary>
    public class DiffResponseMapper : IDiffResponseMapper
    {
        public DiffResponse Map(DiffResult diffResult)
        {
            var diffResponse = new DiffResponse
            {
                EqualsSize = diffResult.SameSize
            };

            diffResponse.Differences = diffResult.Differences.Any() ? diffResult.Differences.Select(x => new Difference(x.StartOffSet, x.Length)) : null;
            diffResponse.DataInfo = new List<DataInfo>
            {
                new DataInfo(diffResult.LeftDataInfo.Id, diffResult.LeftDataInfo.Length),
                new DataInfo(diffResult.RightDataInfo.Id, diffResult.RightDataInfo.Length)
            };

            return diffResponse;
        }
    }
}
