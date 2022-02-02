using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatchList.Domain.Matches;
using MatchList.Infrastructure.Repositories;
using MediatR;

namespace MatchList.Application.Commands.Matches
{
    public class MatchAuditLogHandler : IRequestHandler<MatchAuditLogCommand, bool>
    {
        private readonly IMatchAuditLogRepository _matchAuditLogRepository;

        public MatchAuditLogHandler(IMatchAuditLogRepository matchAuditLogRepository)
        {
            _matchAuditLogRepository = matchAuditLogRepository;
        }

        public async Task<bool> Handle(MatchAuditLogCommand request, CancellationToken cancellationToken)
        {
            var matchAuditLogs = new List<MatchAuditLog>();

            if (!request.OldMatch.EventType.Equals(request.NewMatchModel.EventType))
            {
                matchAuditLogs.Add(new MatchAuditLog(request.OldMatch.EventId, nameof(request.OldMatch.EventType), request.OldMatch.EventType.ToString(), request.NewMatchModel.EventType.ToString()));
            }

            if (!request.OldMatch.Country.Equals(request.NewMatchModel.Country, StringComparison.InvariantCultureIgnoreCase))
            {
                matchAuditLogs.Add(new MatchAuditLog(request.OldMatch.EventId, nameof(request.OldMatch.Country), request.OldMatch.Country, request.NewMatchModel.Country));
            }

            if (!request.OldMatch.League.Equals(request.NewMatchModel.League, StringComparison.InvariantCultureIgnoreCase))
            {
                matchAuditLogs.Add(new MatchAuditLog(request.OldMatch.EventId, nameof(request.OldMatch.League), request.OldMatch.League, request.NewMatchModel.League));
            }

            if (!request.OldMatch.HomeTeam.Equals(request.NewMatchModel.HomeTeam, StringComparison.InvariantCultureIgnoreCase))
            {
                matchAuditLogs.Add(new MatchAuditLog(request.OldMatch.EventId, nameof(request.OldMatch.HomeTeam), request.OldMatch.HomeTeam, request.NewMatchModel.HomeTeam));
            }

            if (!request.OldMatch.AwayTeam.Equals(request.NewMatchModel.AwayTeam, StringComparison.InvariantCultureIgnoreCase))
            {
                matchAuditLogs.Add(new MatchAuditLog(request.OldMatch.EventId, nameof(request.OldMatch.AwayTeam), request.OldMatch.AwayTeam, request.NewMatchModel.AwayTeam));
            }

            if (!request.OldMatch.EventTime.Equals(request.NewMatchModel.EventTime))
            {
                matchAuditLogs.Add(new MatchAuditLog(request.OldMatch.EventId, nameof(request.OldMatch.EventTime), request.OldMatch.EventTime.ToString("G"), request.NewMatchModel.EventTime.ToString("G")));
            }

            if (!matchAuditLogs.Any())
                return false;

            await _matchAuditLogRepository.InsertRangeAsync(matchAuditLogs);
            return true;
        }
    }
}