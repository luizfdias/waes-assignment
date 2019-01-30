using System;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Core.Models;
using SideEnum = Waes.Diff.Core.Models.SideEnum;

namespace Waes.Diff.Api.Factories
{
    public static class DataFactory
    {
        public static Data Create(SaveDataModel saveDataModel)
        {
            return new Data
            {
                Content = saveDataModel.Content,
                CorrelationId = saveDataModel.CorrelationId,
                Length = saveDataModel.Content.Length,
                Side = Enum.Parse<SideEnum>(saveDataModel.Side.ToString()),
                Id = Guid.NewGuid()                
            };
        }
    }
}
