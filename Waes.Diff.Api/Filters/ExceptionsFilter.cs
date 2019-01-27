using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Waes.Diff.Core.Exceptions;

namespace Waes.Diff.Api.Filters
{
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case BinaryDataNotFoundException bdex:
                    context.Result = new JsonResult(new { bdex.Message })
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                    break;                
                default:
                    context.Result = new JsonResult(new { Message = "Unexpected error" })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    break;
            }
        }
    }
}
