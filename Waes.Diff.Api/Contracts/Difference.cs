namespace Waes.Diff.Api.Contracts
{
    public class Difference
    {
        public int StartOffSet { get; }

        public int Length { get; }

        public Difference(int startOffSet, int length)
        {
            StartOffSet = startOffSet;
            Length = length;
        }
    }
}
