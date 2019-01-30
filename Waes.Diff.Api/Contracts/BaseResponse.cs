namespace Waes.Diff.Api.Contracts
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }

        public T Result { get; set; }
    }
}
