namespace Waes.Assignment.Domain.Models
{
    public class DiffInfo
    {
        public int StartIndex { get; }

        public int Length { get; }

        public DiffInfo(int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
        }
    }
}
