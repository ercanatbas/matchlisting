using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchList.Application.Queries.MathAuditLogs
{
    public interface IMatchAuditLogQuery
    {
        Task<List<MatchAuditLogModel>> GetAllMatchLogs(MatchAuditLogListRequest request);
    }
}