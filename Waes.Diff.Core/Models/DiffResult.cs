using System.Collections;

namespace Waes.Diff.Core.Models
{
    public class DiffResult
    {
        public Hashtable Diffs { get; set; }

        public bool SameSize { get; set; }

        public DiffResult()
        {
            Diffs = new Hashtable();
        }        
    }
}
