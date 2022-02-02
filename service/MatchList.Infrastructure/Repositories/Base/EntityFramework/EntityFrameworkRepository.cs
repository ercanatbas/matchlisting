using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MatchList.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchList.Infrastructure.Repositories.Base.EntityFramework
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<TEntity>  _dbSet;

        public EntityFrameworkRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet     = dbContext.Set<TEntity>();
        }

        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        
        public async Task<List<TEntity>> InsertRangeAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public int Count(Expression<Func<TEntity, bool>>                  predicate) => _dbSet.Where(predicate).Count();
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).CountAsync();
    }
}