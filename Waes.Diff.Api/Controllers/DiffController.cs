using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Waes.Diff.Core.Helpers;

namespace Waes.Diff.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        // POST v1/diff/id/left
        [HttpPost("{id}/left")]
        public async Task<IActionResult> PostLeft(string id)
        {
            var result = await new Base64Converter().Convert(Request.Body);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        // POST v1/diff/id/right
        [HttpPost("{id}/right")]
        public async Task<IActionResult> PostRight(string id)
        {
            var result = await new Base64Converter().Convert(Request.Body);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
