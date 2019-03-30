using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Application.ApiModels;
using Waes.Assignment.Application.Interfaces;

namespace Waes.Assignment.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly IPayLoadService _payLoadCreateService;

        private readonly IDiffService _diffAnalyzerService;

        private readonly IResponseCreator _responseCreator;

        public DiffController(IPayLoadService payLoadCreateService, IDiffService diffAnalyzerService,
            IResponseCreator responseHandler) 
        {
            _payLoadCreateService = payLoadCreateService ?? throw new ArgumentNullException(nameof(payLoadCreateService));
            _diffAnalyzerService = diffAnalyzerService ?? throw new ArgumentNullException(nameof(diffAnalyzerService));
            _responseCreator = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        }
       
        [HttpPost("{correlationId}/left")]
        [ProducesResponseType(typeof(SuccessResponse<CreatePayLoadResponse>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]BaseRequest<CreateLeftPayLoadRequest> request)
        {
            var result = await _payLoadCreateService.Create(correlationId, request.Data);

            return _responseCreator.ResponseCreated(result) ?? _responseCreator.ResponseError();
        }

        [HttpPost("{correlationId}/right")]
        [ProducesResponseType(typeof(SuccessResponse<CreatePayLoadResponse>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]BaseRequest<CreateRightPayLoadRequest> request)
        {
            var result = await _payLoadCreateService.Create(correlationId, request.Data);

            return _responseCreator.ResponseCreated(result) ?? _responseCreator.ResponseError();
        }

        [HttpGet("{correlationId}")]
        [ProducesResponseType(typeof(SuccessResponse<EqualResponse>), 200)]
        [ProducesResponseType(typeof(SuccessResponse<NotEqualResponse>), 200)]
        [ProducesResponseType(typeof(SuccessResponse<NotOfEqualSizeResponse>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetDiff([FromRoute]string correlationId)
        {
            var result = await _diffAnalyzerService.Get(correlationId);

            return _responseCreator.ResponseOK(result) ?? _responseCreator.ResponseNotFound();
        }
    }
}
