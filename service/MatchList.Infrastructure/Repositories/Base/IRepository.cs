using System.Collections.Generic;
using System.Threading.Tasks;
using MatchList.Domain.Entities;

namespace MatchList.Infrastructure.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity Insert(TEntity                             entity);
        Task<TEntity> InsertAsync(TEntity                  entity);
        Task<List<TEntity>> InsertRangeAsync(List<TEntity> entities);
    }
}
