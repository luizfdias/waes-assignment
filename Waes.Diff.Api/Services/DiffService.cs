using System.Threading.Tasks;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Factories;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Core.Interfaces;

namespace Waes.Diff.Api.Services
{
    public class DiffService : IHandleRequest<string, BaseResponse<DiffInfo>>
    {
        public IDiffHandler DiffHandler { get; }

        public DiffService(IDiffHandler diffHandler)
        {
            DiffHandler = diffHandler ?? throw new System.ArgumentNullException(nameof(diffHandler));
        }        

        public async Task<BaseResponse<DiffInfo>> Handle(string request)
        {
            var diffResult = await DiffHandler.Diff(request);

            return BaseResponseFactory.Create(diffResult);
        }
    }
}
