using System.Collections.Generic;

namespace Waes.Assignment.Api
{
    public class ResponseWrapper
    {
        public bool Success { get; set; }

        public object Data { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
