using System;

namespace Waes.Diff.Core.Exceptions
{
    /// <summary>
    /// Custom exception to be used when data is not found
    /// </summary>
    public class DataNotFoundException : Exception
    {
        public string Id { get; set; }

        public DataNotFoundException(string id) : base($"Data {id} was not found.")
        {
            Id = id;
        }
    }
}
