namespace Waes.Assignment.Domain.Models
{
    /// <summary>
    /// It represents the result of the diff when comparsion between objects are equal
    /// </summary>
    public class EqualDiff : Diff
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EqualDiff"/> with correlationid
        /// </summary>
        /// <param name="correlationId"></param>
        public EqualDiff(string correlationId) : base(correlationId)
        {
        }
    }
}
