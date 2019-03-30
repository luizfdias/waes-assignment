using System.Collections.Generic;

namespace Waes.Assignment.Application.ApiModels
{
    public class NotEqualResponse : DiffResponse
    {
        public override string Result => "NotEqual";

        public IEnumerable<DiffInfoResponse> Info { get; set; }     
    }
}
