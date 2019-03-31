using System.ComponentModel.DataAnnotations;

namespace Waes.Assignment.Api.Common
{
    /// <summary>
    /// API base request object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRequest<T>
    {
        /// <summary>
        /// The data to be handled
        /// </summary>
        [Required]
        public T Data { get; set; }
    }
}
