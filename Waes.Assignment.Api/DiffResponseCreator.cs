using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Controllers;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Application.ApiModels;

namespace Waes.Assignment.Api
{
    /// <summary>
    /// DiffResponseCreator handles the result and creates a response for the API <see cref="DiffController"/>
    /// </summary>
    public class DiffResponseCreator : IResponseCreator
    {
        /// <summary>
        /// Creates a created response
        /// </summary>
        /// <param name="result"></param>
        /// <returns>If result is not null returns CreatedResult else returns null</returns>
        public IActionResult ResponseCreated(object result)
        {
            if (result == null)
                return null;
            
            var successResponse = new SuccessResponse<CreatePayLoadResponse>
            {
                Data = (CreatePayLoadResponse)result
            };

            return new CreatedResult(string.Empty, successResponse);
        }

        /// <summary>
        /// Creates an ok response
        /// </summary>
        /// <param name="result"></param>
        /// <returns>If result is not null returns OkObjectResult else returns null</returns>
        public IActionResult ResponseOK(object result)
        {
            if (result == null)
                return null;

            return new OkObjectResult(new SuccessResponse<DiffResponse>
            {
                Data = (DiffResponse)result
            });
        }

        /// <summary>
        /// Creates a not found response
        /// </summary>
        /// <returns>NotFoundObjectResult</returns>
        public IActionResult ResponseNotFound()
        {
            var notFoundResponse = new ErrorResponse
            {
                Errors = new List<Error>
                {
                    new Error(ApiCodes.EntityNotFound, "Diff result was not found.")
                }
            };

            return new NotFoundObjectResult(notFoundResponse);
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <returns>ObjectResult with status code 500</returns>
        public IActionResult ResponseError()
        {
            var errorResponse = new ErrorResponse
            {
                Errors = new List<Error>
                {
                    new Error(ApiCodes.OperationFailure, "An error occurred during the operation.")
                }
            };

            return new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
