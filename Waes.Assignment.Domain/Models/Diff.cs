using System;

namespace Waes.Assignment.Domain.Models
{
    /// <summary>
    /// Abstraction for the diffs result
    /// </summary>
    public abstract class Diff : Entity
    {        
        /// <summary>
        /// The correlation id
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Base constructor of <see cref="Diff"/> with correlationid
        /// </summary>
        /// <param name="correlationId"></param>
        public Diff(string correlationId) : base(Guid.NewGuid())
        {
            CorrelationId = correlationId;
        }
    }
}
