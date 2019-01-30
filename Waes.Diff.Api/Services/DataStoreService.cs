using System;
using System.Threading.Tasks;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Factories;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Core.Factories;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Api.Services
{
    public class DataStoreService : IHandleRequest<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>
    {
        public IDataStorageHandler DataStorageHandler { get; }        

        public DataStoreService(IDataStorageHandler dataStorageHandler)
        {
            DataStorageHandler = dataStorageHandler ?? throw new System.ArgumentNullException(nameof(dataStorageHandler));
        }

        public async Task<BaseResponse<SaveDataModel>> Handle(BaseRequest<SaveDataModel> request)
        {
            var data = DataFactory.Create(
                request.Data.Content, 
                request.Data.CorrelationId, 
                Enum.Parse<SideEnum>(request.Data.Side.ToString()));

            await DataStorageHandler.Save(data);

            return BaseResponseFactory.Create(request.Data, data);
        }
    }
}
