namespace MatchList.Infrastructure.Serializing
{
    public interface ISerializer
    {
        public string Serialize<T>(T data);
    }
}