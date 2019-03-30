using Waes.Assignment.Application.Attributes;

namespace Waes.Assignment.Application.ApiModels
{
    public abstract class CreatePayLoadRequest
    {        
        [ContentRequired]
        public byte[] Content { get; set; }
    }
}
