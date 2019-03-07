using System.Collections.Generic;
using Waes.Assignment.Application.ViewModels;

namespace Waes.Assignment.Api.ViewModels
{
    public class NotEqualResponse : DiffResponse
    {
        public override string Result => "NotEqual";

        public IEnumerable<DiffInfoResponse> Info { get; }

        public NotEqualResponse(IEnumerable<DiffInfoResponse> info)
        {
            Info = info;
        }        
    }
}
