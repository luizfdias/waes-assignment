using Waes.Assignment.Application.Attributes;

namespace Waes.Assignment.Application.ApiModels
{
    /// <summary>
    /// Base request object for <see cref="CreateLeftPayLoadRequest"/> and <seealso cref="CreateRightPayLoadRequest"/>    
    /// </summary>
    public abstract class CreatePayLoadRequest
    {        
        /// <summary>
        /// The content to be analyzed
        /// </summary>
        [ContentRequired]
        public byte[] Content { get; set; }
    }
}
