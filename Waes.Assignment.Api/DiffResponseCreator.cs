using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Application.ApiModels;

namespace Waes.Assignment.Api
{
    public class DiffResponseCreator : IResponseCreator
    {        
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

        public IActionResult ResponseOK(object result)
        {
            if (result == null)
                return null;

            return new OkObjectResult(new SuccessResponse<DiffResponse>
            {
                Data = (DiffResponse)result
            });
        }

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
