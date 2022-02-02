using System;
using MatchList.Domain.Entities;

namespace MatchList.Domain.Matches
{
    public class MatchAuditLog : EntityBase<long>
    {
        public long     EventId         { get; set; }
        public string   AffectedColumns { get; set; }
        public string   OldValues       { get; set; }
        public string   NewValues       { get; set; }
        public DateTime AuditOn         { get; set; }

        public MatchAuditLog(long eventId, string affectedColumns, string oldValues, string newValues)
        {
            EventId         = eventId;
            AffectedColumns = affectedColumns;
            OldValues       = oldValues;
            NewValues       = newValues;
            AuditOn         = DateTime.UtcNow;
        }
    }
}