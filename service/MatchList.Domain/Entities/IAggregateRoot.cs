namespace MatchList.Domain.Entities
{
    public interface IAggregateRoot : IAggregateRoot<int>
    {
        
    }

    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
    {

    }
}
