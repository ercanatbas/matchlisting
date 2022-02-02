using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchList.Domain.Matches;
using MatchList.Infrastructure.Repositories.Base.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace MatchList.Infrastructure.Repositories
{
    public class MatchAuditLogRepository : EntityFrameworkRepository<MatchAuditLog>, IMatchAuditLogRepository
    {
        private readonly DatabaseContext _context;

        public MatchAuditLogRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MatchAuditLog>> GetManyMatchAuditLogAsync(long eventId)
        {
            return await _context.MatchAuditLogs.Where(m => m.EventId == eventId).OrderByDescending(m => m.AuditOn).ToListAsync();
        }
    }
}