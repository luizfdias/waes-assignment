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
        public ILogger Logger { get; }

        public ExceptionsFilter(ILogger logger)
        {
            Logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            Logger.Error(context.Exception, "An unexpected error occurred");

            if (context.Exception is EntityAlreadyExistsException entityEx)
            {
                context.Result = GetErrorResult(ApiCodes.EntityAlreadyExists, entityEx.Message, StatusCodes.Status409Conflict);
            }
            else
            {
                context.Result = GetErrorResult(ApiCodes.OperationFailure, "An error occurred during the operation.", StatusCodes.Status500InternalServerError);
            }            

            context.ExceptionHandled = true;
        }

        private static JsonResult GetErrorResult(string code, string message, int statusCode)
        {
            return new JsonResult(
                new ErrorResponse
                {
                    Errors = new List<Error>
                    {
                        new Error(code, message)
                    }
                })
            {
                StatusCode = statusCode
            };
        }
    }
}

