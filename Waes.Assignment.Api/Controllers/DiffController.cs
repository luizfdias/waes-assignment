using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.ViewModels;

namespace Waes.Assignment.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly IPayLoadService _payLoadCreateService;

        private readonly IDiffService _diffAnalyzerService;

        public readonly IResponseHandler _responseHandler;

        public DiffController(IPayLoadService payLoadCreateService, IDiffService diffAnalyzerService,
            IResponseHandler responseHandler) 
        {
            _payLoadCreateService = payLoadCreateService ?? throw new ArgumentNullException(nameof(payLoadCreateService));
            _diffAnalyzerService = diffAnalyzerService ?? throw new ArgumentNullException(nameof(diffAnalyzerService));
            _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        }
       
        [HttpPost("{correlationId}/left")]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]CreateLeftPayLoadRequest request)
        {
            var result = await _payLoadCreateService.Create(correlationId, request);

            return _responseHandler.ResponseCreated(this, result);
        }

        [HttpPost("{correlationId}/right")]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]CreateRightPayLoadRequest request)
        {
            var result = await _payLoadCreateService.Create(correlationId, request);

            return _responseHandler.ResponseCreated(this, result);
        }

        [HttpGet("{correlationId}")]
        public async Task<IActionResult> GetDiff([FromRoute]string correlationId)
        {
            var result = await _diffAnalyzerService.Get(correlationId);

            return _responseHandler.ResponseOK(this, result);
        }
    }
}
