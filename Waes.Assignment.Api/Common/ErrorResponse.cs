using System.Collections.Generic;

namespace Waes.Assignment.Api.Common
{
    /// <summary>
    /// API error response
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// The collection of errors
        /// </summary>
        public IEnumerable<Error> Errors { get; set; }
    }

    /// <summary>
    /// Error representation
    /// </summary>
    public class Error
    {
        /// <summary>
        /// The API Code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The constructor of Error
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }        
    }
}
