using System;

namespace Waes.Diff.Core.Exceptions
{
    /// <summary>
    /// Custom exception to be used when data is not found
    /// </summary>
    public class DataNotFoundException : Exception
    {               
        public DataNotFoundException() : base($"Data was not found")
        {
        }
    }
}
