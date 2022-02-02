using System.Collections.Generic;
using System.Threading.Tasks;
using MatchList.Domain.Matches;
using MatchList.Infrastructure.Repositories.Base;

namespace MatchList.Infrastructure.Repositories
{
    public interface IMatchAuditLogRepository : IRepository<MatchAuditLog>
    {
        Task<List<MatchAuditLog>> GetManyMatchAuditLogAsync(long eventId);
    }
}
