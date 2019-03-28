using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Api.Handlers
{
    public class DiffResponseCreator : IResponseCreator
    {
        private readonly INotificationHandler _notificationHandler;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiffResponseCreator(INotificationHandler notificationHandler, IHttpContextAccessor httpContextAccessor)
        {            
            _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public IActionResult ResponseCreated(object result)
        {
            if (result == null)
                return null;

            var diffAnalyzedEvent = _notificationHandler.GetEvent<DiffAnalyzedEvent>();
            
            var successResponse = new SuccessResponse
            {
                Data = result
            };

            if (diffAnalyzedEvent != null)
            {
                var request = _httpContextAccessor.HttpContext.Request;

                successResponse.Links = new List<Link>
                {
                    new Link
                    {
                        Method = "GET",
                        HRef = $"{request.Scheme}://{request.Host}{request.PathBase}/v1/diff/{diffAnalyzedEvent.CorrelationId}",
                        Rel = "self"
                    }
                };
            }

            return new CreatedResult(successResponse.Links?.FirstOrDefault()?.HRef ?? string.Empty, successResponse);            
        }

        public IActionResult ResponseOK(object result)
        {
            if (result == null)
                return null;

            return new OkObjectResult(new SuccessResponse
            {
                Data = result
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

            return new ObjectResult(errorResponse);
        }
    }
}
