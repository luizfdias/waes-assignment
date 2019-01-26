using System.Collections;
using System.Collections.Generic;

namespace Waes.Diff.Core.Models
{
    public class DiffResult
    {
        public ICollection<DiffData> Diffs { get; set; }

        public bool SameSize { get; set; }

        public DiffResult()
        {
            Diffs = new List<DiffData>();
        }        
    }
}
