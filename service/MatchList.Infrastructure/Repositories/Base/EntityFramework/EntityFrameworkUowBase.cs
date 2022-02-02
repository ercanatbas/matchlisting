using System.Threading;
using System.Threading.Tasks;
using MatchList.Infrastructure.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MatchList.Infrastructure.Repositories.Base.EntityFramework
{
    public class EntityFrameworkUowBase : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;

        public EntityFrameworkUowBase(DbContext context)
        {
            _context = context;
        }
        public virtual void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }
       
        public virtual void Commit()
        {
            _transaction?.Commit();
        }

        public virtual void Rollback()
        {
            _transaction?.Rollback();
        }

        public virtual bool Save()
        {
            return _context.SaveChanges()>0;
        }

        public virtual async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
