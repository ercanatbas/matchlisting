using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchList.Application.Queries.Matches
{
    public interface IMatchQuery
    {
        Task<List<MatchModel>> GetAllMatches();
    }
}