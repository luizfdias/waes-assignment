namespace Waes.Assignment.Application.ApiModels
{
    /// <summary>
    /// Base response object for <see cref="EqualResponse"/>, <seealso cref="NotEqualResponse"/> and <seealso cref="NotOfEqualSizeResponse"/>
    /// </summary>
    public abstract class DiffResponse
    {
        /// <summary>
        /// The status result of the difference
        /// </summary>
        public abstract string Result { get; }
    }
}
