using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Collections.Generic;
using Waes.Assignment.Api.Common;
using Waes.Assignment.Application.Exceptions;

namespace Waes.Assignment.Api.Filters
{
    /// <summary>
    /// An exception filter that handles a generic exception and EntityAlreadyExistsException
    /// </summary>
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionsFilter"/>
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionsFilter(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// It handles the exceptions throwed by the application.
        /// If <see cref="EntityAlreadyExistsException"/> is thrown, it will respond with 409 (conflict)
        /// else another exception is thrown, it will respond with 500 (internal server error)
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            _logger.Error(context.Exception, "An error occurred");

            if (context.Exception is EntityAlreadyExistsException entityEx)
            {
                context.Result = GetErrorResult(ApiCodes.EntityAlreadyExists, entityEx.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            }
            else if (context.Exception is ValidationException validationEx)
            {
                context.Result = GetErrorResult(ApiCodes.InvalidRequest, validationEx.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
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

