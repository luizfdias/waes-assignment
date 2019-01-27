using System;

namespace Waes.Diff.Core.Exceptions
{
    public class BinaryDataNotFoundException : Exception
    {
        public string Id { get; set; }

        public BinaryDataNotFoundException(string id) : base($"Binary data {id} was not found.")
        {
            Id = id;
        }
    }
}
