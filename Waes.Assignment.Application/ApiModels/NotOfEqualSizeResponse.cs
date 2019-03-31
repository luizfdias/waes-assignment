namespace Waes.Assignment.Application.ApiModels
{
    /// <summary>
    /// The NotOfEqualSizeResponse represents the diff result when payloads are not of equal size
    /// </summary>
    public class NotOfEqualSizeResponse : DiffResponse
    {
        /// <summary>
        /// The result: NotOfEqualSize
        /// </summary>
        public override string Result => "NotOfEqualSize";
    }
}
