namespace Waes.Assignment.Application.ApiModels
{
    /// <summary>
    /// Response object with the diff information
    /// </summary>
    public class DiffInfoResponse
    {
        /// <summary>
        /// The start index of the difference
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// The length of the difference
        /// </summary>
        public int Length { get; set; }
    }
}
