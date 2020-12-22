using System;
using System.IO;
using System.Xml.Serialization;
using Common.Enums;
using Common.Interfaces;

namespace SdnListWatcher
{
    public class XmlParser : IXmlParser
    {
        private readonly ILogger _logger;

        public XmlParser(ILogger logger)
        {
            _logger = logger;
        }

        public bool TryDeserializeObject<T>(out T result, string content)
        {
            result = default(T);

            try
            {
                result = DeserializeObject<T>(content);
                return true;
            }
            catch (Exception e)
            {
                _logger.Log($"TryDeserializeObject() => {e.Message}", LogLevel.Fatal);
                return false;
            }
        }

        private T DeserializeObject<T>(string content)
        {
            var serializer = new XmlSerializer(typeof(T));

            using var sr = new StringReader(content);
            return (T) serializer.Deserialize(sr);
        }
    }
}