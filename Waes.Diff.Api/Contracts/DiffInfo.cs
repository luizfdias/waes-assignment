using System.Collections.Generic;
using Waes.Diff.Api.Contracts.Enums;

namespace Waes.Diff.Api.Contracts
{
    public class DiffInfo
    {
        public DiffStatus Status { get; set; }

        public IEnumerable<DataInfo> DataInfo { get; set; }

        public IEnumerable<Difference> Differences { get; set; }
    }
}
