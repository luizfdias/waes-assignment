using System;

namespace Waes.Assignment.Api.ViewModels
{
    public class CreatePayLoadResponse
    {
        public Guid Id { get; set;  }

        public string CorrelationId { get; set; }

        public byte[] Content { get; set; }
    }
}
