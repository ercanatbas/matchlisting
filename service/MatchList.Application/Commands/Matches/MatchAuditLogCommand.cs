using MatchList.Application.Queries.Matches;
using MatchList.Domain.Matches;
using MediatR;

namespace MatchList.Application.Commands.Matches
{
    public class MatchAuditLogCommand : IRequest<bool>
    {
        public Match      OldMatch      { get; set; }
        public MatchModel NewMatchModel { get; set; }

        public MatchAuditLogCommand(Match oldMatch, MatchModel newMatchModel)
        {
            OldMatch      = oldMatch;
            NewMatchModel = newMatchModel;
        }
    }
}