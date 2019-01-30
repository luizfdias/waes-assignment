using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;
using Waes.Diff.Core.Exceptions;

namespace Waes.Diff.Api.Filters
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
            switch (context.Exception)
            {
                case DataNotFoundException dataNotFoundException:
                    context.Result = new JsonResult(new { dataNotFoundException.Message })
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                    break;
                default:
                    {
                        Logger.Error(context.Exception, "An unexpected error occurred");
                        context.Result = new JsonResult(new { Message = "Unexpected error" })
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError
                        };
                        break;
                    }
            }
        }
    }
}
