using System.Collections.Generic;

namespace Waes.Diff.Core.Models
{
    public class DiffResult
    {
        public ICollection<Difference> Differences { get; set; }

        public bool SameSize { get; set; }

        public DataInfo LeftDataInfo { get; set; }

        public DataInfo RightDataInfo { get; set; }

        public DiffResult()
        {
            Differences = new List<Difference>();
        }
    }
}
