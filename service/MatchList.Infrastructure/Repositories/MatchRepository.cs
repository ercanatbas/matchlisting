using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchList.Domain.Matches;
using MatchList.Infrastructure.Repositories.Base.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace MatchList.Infrastructure.Repositories
{
    public class MatchRepository : EntityFrameworkRepository<Match>, IMatchRepository
    {
        private readonly DatabaseContext _context;

        public MatchRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetManyMatchAsync()
        {
            return await _context.Matches.AsNoTracking().ToListAsync();
        }

        public async Task<List<Match>> GetManyMatchAsync(List<long> eventIds)
        {
            return await _context.Matches.Where(m => eventIds.Contains(m.EventId)).ToListAsync();
        }
    }
}