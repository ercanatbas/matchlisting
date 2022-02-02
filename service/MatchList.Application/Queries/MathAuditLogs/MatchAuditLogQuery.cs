using System.Collections.Generic;
using System.Threading.Tasks;
using MatchList.Infrastructure.Repositories;
using MatchList.Infrastructure.Services.Map;

namespace MatchList.Application.Queries.MathAuditLogs
{
    public class MatchAuditLogQuery : IMatchAuditLogQuery
    {
        private readonly IMatchAuditLogRepository _matchAuditLogRepository;
        private readonly IMapperService           _mapper;

        public MatchAuditLogQuery(IMatchAuditLogRepository matchAuditLogRepository, IMapperService mapper)
        {
            _matchAuditLogRepository = matchAuditLogRepository;
            _mapper                  = mapper;
        }

        public async Task<List<MatchAuditLogModel>> GetAllMatchLogs(MatchAuditLogListRequest request)
        {
            var matchAuditLogs = await _matchAuditLogRepository.GetManyMatchAuditLogAsync(request.EventId);
            return _mapper.Map<List<MatchAuditLogModel>>(matchAuditLogs);
        }
    }
}