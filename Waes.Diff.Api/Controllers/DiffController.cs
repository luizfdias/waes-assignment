using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Contracts.Enums;
using Waes.Diff.Api.Interfaces;

namespace Waes.Diff.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        public IMediator Mediator { get; }

        public DiffController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // POST v1/diff/id/left
        /// <summary>
        /// Stores the left data to be analyzed
        /// </summary>
        /// <param name="id">The identification</param>
        [HttpPost("{correlationId}/left")]
        public async Task<IActionResult> PostLeft(string correlationId, [FromBody]BaseRequest<SaveDataModel> request)
        {
            request.Data.CorrelationId = correlationId;
            request.Data.Side = SideEnum.Left;

            var result = await Mediator.Send<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>(request);

            return CreatedAtAction(nameof(GetDiff), new { correlationId }, result);
        }

        // POST v1/diff/id/right
        /// <summary>
        /// Stores the right data to be analyzed
        /// </summary>
        /// <param name="id">The identification</param>
        [HttpPost("{correlationId}/right")]
        public async Task<IActionResult> PostRight(string correlationId, [FromBody]BaseRequest<SaveDataModel> request)
        {
            request.Data.CorrelationId = correlationId;
            request.Data.Side = SideEnum.Right;

            var result = await Mediator.Send<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>(request);

            return CreatedAtAction(nameof(GetDiff), new { correlationId }, result);
        }

        // GET v1/diff/id
        /// <summary>
        /// Get the differences between the data
        /// </summary>
        /// <param name="id">The identification</param>
        /// <returns>The DiffResponse</returns>
        [HttpGet("{correlationId}")]
        public async Task<IActionResult> GetDiff(string correlationId)
        {
            var result = await Mediator.Send<string, BaseResponse<DiffInfo>>(correlationId);

            return Ok(result);
        }
    }
}
