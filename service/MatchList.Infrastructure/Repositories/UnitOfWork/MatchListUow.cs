using System;
using System.Threading;
using System.Threading.Tasks;
using MatchList.Infrastructure.Extensions;
using MatchList.Infrastructure.Repositories.Base.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MatchList.Infrastructure.Repositories.UnitOfWork
{
    public class MatchListUow : EntityFrameworkUowBase
    {
        private readonly DbContext _context;
        private readonly IMediator _mediator;

        public MatchListUow(DbContext context, IMediator mediator) : base(context)
        {
            _context  = context ?? throw new ArgumentNullException(nameof(context));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(_context);
            return await base.SaveAsync(cancellationToken);
        }
    }
}