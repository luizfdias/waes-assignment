using System.Collections.Generic;

namespace Waes.Assignment.Application.ApiModels
{
    /// <summary>
    /// The NotEqualResponse represents the diff result when payloads are not equal
    /// </summary>
    public class NotEqualResponse : DiffResponse
    {
        /// <summary>
        /// The result: NotEqual
        /// </summary>
        public override string Result => "NotEqual";

        /// <summary>
        /// The information of the differences
        /// </summary>
        public IEnumerable<DiffInfoResponse> Info { get; set; }     
    }
}
