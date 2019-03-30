using System;
using System.Threading.Tasks;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetById(Guid id);

        Task Add(TEntity item);

        Task Remove(TEntity item);        

        Task Update(TEntity item);
    }
}
