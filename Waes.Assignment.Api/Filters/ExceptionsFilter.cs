﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;

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
            context.Result = new JsonResult(
                new
                {
                    Errors = new[]
                    {
                        new
                        {
                            Code = "999",
                            Message = "An error occurred during the operation."
                        }
                    }
                })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}

