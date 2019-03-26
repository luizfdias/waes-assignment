using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Collections.Generic;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Application.Exceptions;

namespace Waes.Assignment.Api.Filters
{
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ExceptionsFilter(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.Error(context.Exception, "An unexpected error occurred");

            if (context.Exception is EntityAlreadyExistsException entityEx)
            {
                context.Result = GetErrorResult(ApiCodes.EntityAlreadyExists, entityEx.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            }
            else
            {
                context.Result = GetErrorResult(ApiCodes.OperationFailure, "An error occurred during the operation.");
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }            

            context.ExceptionHandled = true;
        }

        private static JsonResult GetErrorResult(string code, string message)
        {
            return new JsonResult(
                new ErrorResponse
                {
                    Errors = new List<Error>
                    {
                        new Error(code, message)
                    }
                });
        }
    }
}

