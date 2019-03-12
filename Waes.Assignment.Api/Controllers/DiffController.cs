using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Application.Notifications;
using Waes.Assignment.Application.Notifications.Interfaces;
using Waes.Assignment.Application.ViewModels;

namespace Waes.Assignment.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ApiController
    {
        private readonly IPayLoadCreateService _payLoadCreateService;

        private readonly IDiffAnalyzerService _diffAnalyzerService;

        public DiffController(IPayLoadCreateService payLoadCreateService, IDiffAnalyzerService diffAnalyzerService,
            IGetNotifications<WarningNotification> notificationHandler) : base(notificationHandler)
        {
            _payLoadCreateService = payLoadCreateService ?? throw new ArgumentNullException(nameof(payLoadCreateService));
            _diffAnalyzerService = diffAnalyzerService ?? throw new ArgumentNullException(nameof(diffAnalyzerService));
        }

        // POST v1/diff/id/left
        /// <summary>
        /// Stores the left data to be analyzed
        /// </summary>
        /// <param name="id">The identification</param>
        [HttpPost("{correlationId}/left")]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]CreateLeftPayLoadRequest request)
        {
            var result = await _payLoadCreateService.CreateNewPayload(correlationId, request);

            return ResponseCreated("", result);
        }

        // POST v1/diff/id/left
        /// <summary>
        /// Stores the left data to be analyzed
        /// </summary>
        /// <param name="id">The identification</param>
        [HttpPost("{correlationId}/right")]
        public async Task<IActionResult> Post([FromRoute]string correlationId, [FromBody]CreateRightPayLoadRequest request)
        {
            var result = await _payLoadCreateService.CreateNewPayload(correlationId, request).ConfigureAwait(false);

            return ResponseCreated("", result);
        }

        // GET v1/diff/id
        /// <summary>
        /// Get the differences between the data
        /// </summary>
        /// <param name="id">The identification</param>
        /// <returns>The DiffResponse</returns>
        [HttpGet("{correlationId}")]
        public async Task<IActionResult> GetDiff([FromRoute]string correlationId)
        {
            var result = await _diffAnalyzerService.Analyze(correlationId);

            return ResponseOK(result);
        }
    }
}
