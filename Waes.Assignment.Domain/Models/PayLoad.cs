using System;
using Waes.Assignment.Domain.Models.Abstractions;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Models
{
    public class PayLoad : IDifferableList
    {
        public Guid Id { get; }

        public string CorrelationId { get; }

        public byte[] Content { get; }

        public IComparable this[int index] => Content[index];

        public SideEnum Side { get; }

        protected PayLoad() { }

        public PayLoad(Guid id, string correlationId, byte[] content, SideEnum side)
        {
            Id = id;
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }

        public int Count()
        {
            return Content.Length;
        }
    }
}
