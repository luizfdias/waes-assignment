using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Models
{
    public class PayLoad : Entity
    {
        public string CorrelationId { get; set; }

        public byte[] Content { get; set; }        

        public SideEnum Side { get; set; }

        public int GetSize()
        {
            return Content.Length;
        }
    }
}
