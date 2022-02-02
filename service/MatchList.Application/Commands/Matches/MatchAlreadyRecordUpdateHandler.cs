using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatchList.Application.Queries.Matches;
using MatchList.Infrastructure.Repositories;
using MediatR;

namespace MatchList.Application.Commands.Matches
{
    public class MatchAlreadyRecordUpdateHandler : IRequestHandler<MatchAlreadyRecordUpdateCommand, List<MatchModel>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMediator        _mediator;

        public MatchAlreadyRecordUpdateHandler(IMatchRepository matchRepository, IMediator mediator)
        {
            _matchRepository = matchRepository;
            _mediator        = mediator;
        }

        public async Task<List<MatchModel>> Handle(MatchAlreadyRecordUpdateCommand request, CancellationToken cancellationToken)
        {
            var matchModels = request.MatchModels;
            var eventIds        = matchModels.Select(m => m.EventId).Distinct().ToList();
            var existingMatches = await _matchRepository.GetManyMatchAsync(eventIds);

            if (existingMatches.Any())
            {
                var existingMatchIds = existingMatches.Select(m => m.EventId).ToList();
                foreach (var newMatchModel in matchModels.Where(m => existingMatchIds.Contains(m.EventId)))
                {
                    var existingMatch = existingMatches.FirstOrDefault(m => m.EventId == newMatchModel.EventId);
                    if (existingMatch is null)
                        continue;

                    await _mediator.Send(new MatchAuditLogCommand(existingMatch, newMatchModel), cancellationToken);

                    existingMatch.Update(newMatchModel.EventType, newMatchModel.Country, newMatchModel.League, newMatchModel.HomeTeam, newMatchModel.AwayTeam, newMatchModel.EventTime);
                    newMatchModel.SetProcessed();
                }
            }

            return matchModels.Where(m => !m.IsProcessed).ToList();
        }
    }
}