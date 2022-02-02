using AutoMapper;
using MatchList.Application.Queries.Matches;
using MatchList.Application.Queries.MathAuditLogs;
using MatchList.Domain.Matches;

namespace MatchList.Application.Maps
{
    public class MatchMap : Profile
    {
        public MatchMap()
        {
            CreateMap<MatchModel, Match>();
            CreateMap<Match, MatchModel>();

            CreateMap<MatchAuditLog, MatchAuditLogModel>();
        }
    }
}