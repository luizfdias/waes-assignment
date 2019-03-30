using System.ComponentModel.DataAnnotations;

namespace Waes.Assignment.Api.Common
{
    public class BaseRequest<T>
    {
        [Required]
        public T Data { get; set; }
    }
}
