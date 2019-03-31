namespace Waes.Assignment.Api.Common
{
    /// <summary>
    /// It contains all API codes
    /// </summary>
    public class ApiCodes
    {
        /// <summary>
        /// Used when an entity already exists
        /// </summary>
        public const string EntityAlreadyExists = "800";

        /// <summary>
        /// Used when an entity was not found
        /// </summary>
        public const string EntityNotFound = "801";

        /// <summary>
        /// Used for invalid requests
        /// </summary>
        public const string InvalidRequest = "900";

        /// <summary>
        /// Used for unhandled failures
        /// </summary>
        public const string OperationFailure = "999";
    }
}
