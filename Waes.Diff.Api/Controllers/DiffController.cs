using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;

namespace Waes.Diff.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        public IBinaryStorageHandler BinaryStorageHandler { get; }

        public IDiffHandler DiffHandler { get; }

        public DiffController(IBinaryStorageHandler binaryStorageHandler, IDiffHandler diffHandler)
        {
            BinaryStorageHandler = binaryStorageHandler ?? throw new ArgumentNullException(nameof(binaryStorageHandler));
            DiffHandler = diffHandler ?? throw new ArgumentNullException(nameof(diffHandler));
        }

        // POST v1/diff/id/left
        [HttpPost("{id}/left")]
        public async Task<IActionResult> PostLeft(string id)
        {
            await BinaryStorageHandler.Save("left" + id, Request.Body);
            
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        // POST v1/diff/id/right
        [HttpPost("{id}/right")]
        public async Task<IActionResult> PostRight(string id)
        {
            await BinaryStorageHandler.Save("right" + id, Request.Body);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {            
            return Ok(await DiffHandler.Diff(id));
        }
    }
}
