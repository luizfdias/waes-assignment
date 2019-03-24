using Waes.Assignment.Application.Attributes;

namespace Waes.Assignment.Api.ViewModels
{
    public abstract class CreatePayLoadRequest
    {        
        [ContentRequired]
        public byte[] Content { get; set; }
    }
}
