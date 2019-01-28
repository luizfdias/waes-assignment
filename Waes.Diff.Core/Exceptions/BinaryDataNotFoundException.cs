using System;

namespace Waes.Diff.Core.Exceptions
{
    /// <summary>
    /// Custom exception to be used when binary data is not found
    /// </summary>
    public class BinaryDataNotFoundException : Exception
    {
        public string Id { get; set; }

        public BinaryDataNotFoundException(string id) : base($"Binary data {id} was not found.")
        {
            Id = id;
        }
    }
}
