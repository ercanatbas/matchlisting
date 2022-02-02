using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatchList.Application.Queries.Matches;
using MatchList.Domain.Matches;
using MatchList.Infrastructure.Services.Map;
using MediatR;

namespace MatchList.Application.Commands.Matches
{
    public class MatchDuplicateRecordParseHandler : IRequestHandler<MatchDuplicateRecordParseCommand, List<MatchModel>>
    {
        private readonly IMediator      _mediator;
        private readonly IMapperService _mapper;

        public MatchDuplicateRecordParseHandler(IMediator mediator, IMapperService mapper)
        {
            _mediator = mediator;
            _mapper   = mapper;
        }

        public async Task<List<MatchModel>> Handle(MatchDuplicateRecordParseCommand request, CancellationToken cancellationToken)
        {
            var matchModels = request.MatchModels;
            var sameMatchGroup = matchModels.Where(m => !m.IsProcessed).GroupBy(m => m.EventId)
                                            .Where(g => g.Count() > 1).Select(y => new { EventId = y.Key, Count = y.Count() }).ToList();

            MatchModel currentMatchModel = null;
            foreach (var sameMatch in sameMatchGroup)
            {
                for (var i = 0; i < sameMatch.Count; i++)
                {
                    if (currentMatchModel != null)
                    {
                        var newMatchModel = matchModels.FirstOrDefault(m => m.EventId == sameMatch.EventId && !m.IsProcessed);
                        if (newMatchModel == null)
                            continue;

                        var currentMatch = _mapper.Map<Match>(currentMatchModel);
                        await _mediator.Send(new MatchAuditLogCommand(currentMatch, newMatchModel), cancellationToken);

                        currentMatchModel.Update(newMatchModel.EventType, newMatchModel.Country, newMatchModel.League, newMatchModel.HomeTeam, newMatchModel.AwayTeam, newMatchModel.EventTime);
                        newMatchModel.SetProcessed();
                    }
                    else
                    {
                        currentMatchModel = matchModels.FirstOrDefault(m => m.EventId == sameMatch.EventId && !m.IsProcessed);
                        if (currentMatchModel != null)
                            currentMatchModel.SetProcessed();
                    }
                }

                matchModels.RemoveAll(m => m.EventId == sameMatch.EventId);
                matchModels.Add(currentMatchModel);

                currentMatchModel = null;
            }

            return matchModels;
        }
    }
}