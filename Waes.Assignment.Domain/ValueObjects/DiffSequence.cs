namespace Waes.Assignment.Domain.ValueObjects
{
    public class DiffSequence
    {
        public int StartIndex { get; }
        public int Length { get; }

        public DiffSequence(int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
        }        
    }
}
