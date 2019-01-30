using System.Threading.Tasks;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Factories;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Core.Interfaces;

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
            var data = DataFactory.Create(request.Request);

            await DataStorageHandler.Save(data);

            return BaseResponseFactory.Create(request.Request, data);
        }
    }
}
