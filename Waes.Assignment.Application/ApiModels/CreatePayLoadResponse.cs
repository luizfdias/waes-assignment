using System;

namespace Waes.Assignment.Application.ApiModels
{
    /// <summary>
    /// Response object for create new payload operation
    /// </summary>
    public class CreatePayLoadResponse
    {
        /// <summary>
        /// The generated id
        /// </summary>
        public Guid Id { get; set;  }

        /// <summary>
        /// The given correlation id
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// The content to be analyzed
        /// </summary>
        public byte[] Content { get; set; }
    }
}
