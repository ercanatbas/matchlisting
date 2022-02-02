using System.Threading;
using System.Threading.Tasks;

namespace MatchList.Infrastructure.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        bool Save();
        Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
