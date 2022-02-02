using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MatchList.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MatchList.Infrastructure.Extensions
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<IEntity>()
                .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Events)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
