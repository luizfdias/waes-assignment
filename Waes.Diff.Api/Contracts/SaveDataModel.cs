using System;
using Waes.Diff.Api.Contracts.Enums;

namespace Waes.Diff.Api.Contracts
{
    public class SaveDataModel
    {
        public Guid? Id { get; set; }

        public string CorrelationId { get; set; }

        public byte[] Content { get; set; }

        public SideEnum Side { get; set; }
    }
}
