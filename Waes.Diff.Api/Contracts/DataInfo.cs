namespace Waes.Diff.Api.Contracts
{
    public class DataInfo
    {
        public string Id { get; }

        public int Length { get; }

        public DataInfo(string id, int length)
        {
            Id = id;
            Length = length;
        }
    }
}
