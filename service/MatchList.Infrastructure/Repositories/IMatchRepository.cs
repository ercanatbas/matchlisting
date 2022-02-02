using System.Collections.Generic;
using System.Threading.Tasks;
using MatchList.Domain.Matches;
using MatchList.Infrastructure.Repositories.Base;

namespace MatchList.Infrastructure.Repositories
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task<List<Match>> GetManyMatchAsync();
        Task<List<Match>> GetManyMatchAsync(List<long> eventIds);
    }
}
