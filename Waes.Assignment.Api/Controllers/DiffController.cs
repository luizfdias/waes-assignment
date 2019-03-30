using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Creates a left payload.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /fcfbda03-06df-4a3f-8e31-7eeaf0c004e4/left
        ///     {
        ///        "data": {
        ///            "content":"YWJjMTIz"
        ///        }
        ///     }
        /// </remarks>
        /// <param name="correlationId"></param>
        /// <param name="request"></param>
        /// <returns>A newly created payload</returns>
        /// <response code="201">Returns the newly created payload</response>
        /// <response code="400">If the content is null</response> 
        /// <response code="409">If the payload already exists</response> 
        [HttpPost("{correlationId}/left")]
        [ProducesResponseType(typeof(SuccessResponse<CreatePayLoadResponse>), StatusCodes.Status201Created)]        
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]BaseRequest<CreateLeftPayLoadRequest> request)
        {
            var result = await _payLoadCreateService.Create(correlationId, request.Data);

            return _responseCreator.ResponseCreated(result) ?? _responseCreator.ResponseError();
        }

        /// <summary>
        /// Creates a left payload.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /fcfbda03-06df-4a3f-8e31-7eeaf0c004e4/right
        ///     {
        ///        "data": {
        ///            "content":"YWJjMTIz"
        ///        }
        ///     }
        /// </remarks>
        /// <param name="correlationId"></param>
        /// <param name="request"></param>
        /// <returns>A newly created payload</returns>
        /// <response code="201">Returns the newly created payload</response>
        /// <response code="400">If the content is null</response> 
        /// <response code="409">If the payload already exists</response> 
        [HttpPost("{correlationId}/right")]
        [ProducesResponseType(typeof(SuccessResponse<CreatePayLoadResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]BaseRequest<CreateRightPayLoadRequest> request)
        {
            var result = await _payLoadCreateService.Create(correlationId, request.Data);

            return _responseCreator.ResponseCreated(result) ?? _responseCreator.ResponseError();
        }

        /// <summary>
        /// Gets diff result.
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        /// GET /fcfbda03-06df-4a3f-8e31-7eeaf0c004e4
        /// </remarks>
        /// <param name="correlationId"></param>
        /// <returns>A diff result between two payloads</returns>
        /// <response code="200">Returns the diff result</response>
        /// <response code="404">If the diff result is not found</response> 
        [HttpGet("{correlationId}")]
        [ProducesResponseType(typeof(SuccessResponse<EqualResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse<NotEqualResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse<NotOfEqualSizeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDiff([FromRoute]string correlationId)
        {
            var result = await _diffAnalyzerService.Get(correlationId);

            return _responseCreator.ResponseOK(result) ?? _responseCreator.ResponseNotFound();
        }
    }
}
