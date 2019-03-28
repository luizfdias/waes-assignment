namespace Waes.Assignment.Domain.ValueObjects
{
    public class Differences
    {
        public int StartIndex { get; }
        public int Length { get; }

        public Differences(int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
        }        
    }
}
