using System;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Infrastructure.MongoDBStorage.Exceptions
{
    public class CorrelationIdAlreadyUsedForDataException : Exception
    {
        public CorrelationIdAlreadyUsedForDataException(string correlationId, SideEnum side) : base($"Correlation id '{correlationId}' already used for data in '{side}' side")
        {
        }
    }
}
