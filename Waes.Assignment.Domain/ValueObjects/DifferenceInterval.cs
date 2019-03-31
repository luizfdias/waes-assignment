namespace Waes.Assignment.Domain.ValueObjects
{
    /// <summary>
    /// It represents the interval of differences
    /// </summary>
    public class DifferenceInterval
    {
        /// <summary>
        /// The start index of the difference
        /// </summary>
        public int StartIndex { get; }

        /// <summary>
        /// The length of the difference
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="DifferenceInterval"/> with start index and length
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        public DifferenceInterval(int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
        }        
    }
}
