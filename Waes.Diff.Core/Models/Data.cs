using System;

namespace Waes.Diff.Core.Models
{
    public class Data
    {
        public Guid Id { get; set; }

        public string CorrelationId { get; set; }

        public byte[] Content { get; set; }

        public int Length { get; set; }

        public SideEnum Side { get; set; }
    }
}
