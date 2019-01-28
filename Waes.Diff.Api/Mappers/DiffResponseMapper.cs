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
            var diffResponse = new DiffResponse()
            {                
                DataInfo = new List<DataInfo>
                {
                    new DataInfo(diffResult.LeftDataInfo.Id, diffResult.LeftDataInfo.Length),
                    new DataInfo(diffResult.RightDataInfo.Id, diffResult.RightDataInfo.Length)
                }
            };

            if (!diffResult.SameSize)
            {
                diffResponse.Code = ApiReturns.NotOfEqualSize.Code;
                diffResponse.Message = ApiReturns.NotOfEqualSize.Message;

                return diffResponse;
            }

            if (diffResult.Differences.Any())
            {
                diffResponse.Differences = diffResult.Differences.Select(x => new Difference(x.StartOffSet, x.Length));
                diffResponse.Code = ApiReturns.NotEqual.Code;
                diffResponse.Message = ApiReturns.NotEqual.Message;

                return diffResponse;
            }

            diffResponse.Code = ApiReturns.Equal.Code;
            diffResponse.Message = ApiReturns.Equal.Message;

            return diffResponse;
        }
    }
}
