using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MatchList.Infrastructure.Serializing
{
    public class SerializeToJson : ISerializer, IDeSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize<T>(IFormFile data)
        {
            string fileContent;
            using (var reader = new StreamReader(data.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}