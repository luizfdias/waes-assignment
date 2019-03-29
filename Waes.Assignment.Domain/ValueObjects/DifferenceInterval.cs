namespace Waes.Assignment.Domain.ValueObjects
{
    public class DifferenceInterval
    {
        public int StartIndex { get; }
        public int Length { get; }

        public DifferenceInterval(int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
        }        
    }
}
