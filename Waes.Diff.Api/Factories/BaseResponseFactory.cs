using System.Linq;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Contracts.Enums;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Api.Factories
{
    public static class BaseResponseFactory
    {
        public static BaseResponse<SaveDataModel> Create(SaveDataModel saveData, Data data)
        {
            saveData.Id = data.Id;

            return new BaseResponse<SaveDataModel>
            {
                Result = saveData,
                Success = true
            };
        }

        public static BaseResponse<DiffInfo> Create(DiffResult diffResult)
        {            
            var diffInfo = new DiffInfo();

            var baseResponse = new BaseResponse<DiffInfo>
            {
                Result = diffInfo,
                Success = true
            };

            diffInfo.DataInfo = diffResult.Data.Select(x => DataInfoFactory.Create(x));

            if (!diffResult.SameSize)
            {
                diffInfo.Status = DiffStatus.NotOfEqualSize;
                return baseResponse;
            }

            if (diffResult.Differences.Any())
            {
                diffInfo.Status = DiffStatus.NotEqual;
                diffInfo.Differences = diffResult.Differences.Select(x => new Contracts.Difference(x.StartOffSet, x.Length));

                return baseResponse;
            }

            diffInfo.Status = DiffStatus.Equal;
            return baseResponse;
        }
    }
}
