namespace Waes.Diff.Core.Interfaces
{
    public interface IRepository
    {
        bool Save<T>(T data);

        T Get<T>(string id);
    }
}
