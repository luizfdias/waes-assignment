using System.Collections.Generic;
using System.Linq;

namespace Waes.Assignment.Domain.Models
{
    public class Diff 
    {        
        public IEnumerable<DiffInfo> Info { get; internal set; }        

        public bool HasDiff()
        {
            return Info.Any();
        }
    }
}
