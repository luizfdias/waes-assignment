using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Core.Interfaces;

namespace Waes.Diff.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        public IBinaryStorageHandler BinaryStorageHandler { get; }

        public IDiffHandler DiffHandler { get; }

        public IDiffResponseMapper DiffResponseMapper { get; }

        public DiffController(IBinaryStorageHandler binaryStorageHandler, IDiffHandler diffHandler, IDiffResponseMapper diffResponseMapper)
        {
            BinaryStorageHandler = binaryStorageHandler ?? throw new ArgumentNullException(nameof(binaryStorageHandler));
            DiffHandler = diffHandler ?? throw new ArgumentNullException(nameof(diffHandler));
            DiffResponseMapper = diffResponseMapper ?? throw new ArgumentNullException(nameof(diffResponseMapper));
        }

        // POST v1/diff/id/left
        /// <summary>
        /// Stores the left data to be analyzed
        /// </summary>
        /// <param name="id">The identification</param>
        [HttpPost("{id}/left")]
        public async Task<IActionResult> PostLeft(string id)
        {            
            await BinaryStorageHandler.Save($"left_{id}", Request.Body);
            
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        // POST v1/diff/id/right
        /// <summary>
        /// Stores the right data to be analyzed
        /// </summary>
        /// <param name="id">The identification</param>
        [HttpPost("{id}/right")]
        public async Task<IActionResult> PostRight(string id)
        {
            await BinaryStorageHandler.Save($"right_{id}", Request.Body);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        // GET v1/diff/id
        /// <summary>
        /// Get the differences between the datas
        /// </summary>
        /// <param name="id">The identification</param>
        /// <returns>The DiffResponse</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {            
            var result = await DiffHandler.Diff(id);

            return Ok(DiffResponseMapper.Map(result));
        }
    }
}
