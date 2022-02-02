using System;

namespace MatchList.Application.Queries.MathAuditLogs
{
    public class MatchAuditLogModel
    {
        public long     Id              { get; set; }
        public string   AffectedColumns { get; set; }
        public string   OldValues       { get; set; }
        public string   NewValues       { get; set; }
        public DateTime AuditOn         { get; set; }
    }
}