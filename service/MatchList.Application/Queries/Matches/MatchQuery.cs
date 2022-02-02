using System.Collections.Generic;
using System.Threading.Tasks;
using MatchList.Infrastructure.Repositories;
using MatchList.Infrastructure.Services.Map;

namespace MatchList.Application.Queries.Matches
{
    public class MatchQuery : IMatchQuery
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapperService   _mapper;

        public MatchQuery(IMatchRepository matchRepository, IMapperService mapper)
        {
            _matchRepository = matchRepository;
            _mapper          = mapper;
        }

        public async Task<List<MatchModel>> GetAllMatches()
        {
            var matches = await _matchRepository.GetManyMatchAsync();
            return _mapper.Map<List<MatchModel>>(matches);
        }
    }
}