using System.Collections.Generic;

namespace Waes.Assignment.Api.Common
{
    public class SuccessResponse<T>
    {
        public T Data { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }

    public class Link
    {
        public string Method { get; set; }

        public string HRef { get; set; }

        public string Rel { get; set; }
    }
}
