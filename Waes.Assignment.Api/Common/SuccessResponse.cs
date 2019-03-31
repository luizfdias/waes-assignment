namespace Waes.Assignment.Api.Common
{
    /// <summary>
    /// API success response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuccessResponse<T>
    {
        /// <summary>
        /// The data to be responsed
        /// </summary>
        public T Data { get; set; }
    }
}
