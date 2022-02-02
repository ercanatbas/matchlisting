using Microsoft.AspNetCore.Http;

namespace MatchList.Infrastructure.Serializing
{
    public interface IDeSerializer
    {
        public T Deserialize<T>(IFormFile data);
    }
}