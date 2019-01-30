using System;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Api.Factories
{
    public static class DataInfoFactory
    {
        public static DataInfo Create(Data data)
        {
            return new DataInfo
            {
                Id = data.Id,
                CorrelationId = data.CorrelationId,
                Length = data.Length,
                Side = Enum.Parse<Contracts.Enums.SideEnum>(data.Side.ToString())
            };
        }
    }
}
