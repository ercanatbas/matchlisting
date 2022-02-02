using System.IO;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;

namespace MatchList.Infrastructure.Serializing
{
    public class SerializeToXml : ISerializer, IDeSerializer
    {
        public string Serialize<T>(T data)
        {
            var       xmlSerializer = new XmlSerializer(typeof(T));
            using var stringWriter  = new StringWriter();
            xmlSerializer.Serialize(stringWriter, data);
            return stringWriter.ToString();
        }

        public T Deserialize<T>(IFormFile data)
        {
            var       serializer = new XmlSerializer(typeof(T));
            using var reader     = new StreamReader(data.OpenReadStream());
            return (T) serializer.Deserialize(reader);
        }
    }
}