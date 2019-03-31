using System;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Models
{
    /// <summary>
    /// It represents the data to be compared
    /// </summary>
    public class PayLoad : Entity
    {
        /// <summary>
        /// The correlation id
        /// </summary>
        public string CorrelationId { get; private set; }

        /// <summary>
        /// The content
        /// </summary>
        public byte[] Content { get; private set; }        

        /// <summary>
        /// The side
        /// </summary>
        public SideEnum Side { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="PayLoad"/> with correlationid, content and side
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="content"></param>
        /// <param name="side"></param>
        public PayLoad(string correlationId, byte[] content, SideEnum side) : base(Guid.NewGuid())
        {
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }
    }
}
