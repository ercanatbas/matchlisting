using FluentValidation;
using Microsoft.Extensions.Logging;

namespace MatchList.Application.Queries.MathAuditLogs
{
    public class MatchAuditLogListRequest
    {
        public long EventId { get; set; }
    }

    public class MatchAuditLogListRequestValidation : AbstractValidator<MatchAuditLogListRequest>
    {
        public MatchAuditLogListRequestValidation(ILogger<MatchAuditLogListRequestValidation> logger)
        {
            RuleFor(log => log).NotNull().WithMessage("Your request cannot be null or empty.");
            RuleFor(log => log.EventId).NotNull().WithMessage("EventId not found")
                                       .GreaterThan(0).WithMessage("EventId must be greater than zero.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}