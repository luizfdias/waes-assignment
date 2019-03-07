using System.Collections.Generic;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Infrastructure.Databases.InMemory
{
    public class InMemoryDatabase 
    {
        public ICollection<PayLoad> PayLoads { get; private set; } = new List<PayLoad>();
    }
}
