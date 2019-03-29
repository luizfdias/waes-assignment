using System.Collections.Generic;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Infra.Interfaces
{
    public interface IDatabase<TEntity> where TEntity : Entity
    {
        ICollection<TEntity> Entities { get; }
    }
}
