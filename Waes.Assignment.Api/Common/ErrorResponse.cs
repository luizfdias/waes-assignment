using System.Collections.Generic;

namespace Waes.Assignment.Api.Common
{
    public class ErrorResponse
    {
        public IEnumerable<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; }

        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }        
    }
}
