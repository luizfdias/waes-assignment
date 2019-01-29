namespace Waes.Diff.Api.Contracts
{
    public static class ApiReturns
    {
        public static (string Code, string Message) Equal => ("00", "Equal");

        public static (string Code, string Message) NotEqual => ("01", "Not equal");

        public static (string Code, string Message) NotOfEqualSize => ("02", "Not of equal size");
    }
}
