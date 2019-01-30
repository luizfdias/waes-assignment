using System.Collections.Generic;

namespace Waes.Diff.Core.Models
{
    public class DiffResult
    {
        public ICollection<Difference> Differences { get; set; }

        public bool SameSize { get; set; }

        public IEnumerable<Data> Data { get; set; }        

        public DiffResult()
        {
            Differences = new List<Difference>();
        }
    }
}
