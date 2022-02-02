using System.Collections.Generic;
using MediatR;

namespace MatchList.Domain.Entities
{
    public interface IEntity
    {
        IEnumerable<INotification> Events { get; }
        void ClearEvents();
        void AddEvent(INotification @event);
        bool IsTransient();
        object GetPrimaryKey();
    }
    public interface IEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get; }
    }
}