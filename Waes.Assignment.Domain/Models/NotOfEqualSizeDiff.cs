namespace Waes.Assignment.Domain.Models
{
    /// <summary>
    /// It represents the result of the diff when comparsion between objects are not of equal size
    /// </summary>
    public class NotOfEqualSizeDiff : Diff
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NotOfEqualSizeDiff"/> with correlationid
        /// </summary>
        /// <param name="correlationId"></param>
        public NotOfEqualSizeDiff(string correlationId) : base(correlationId)
        {
        }
    }
}
