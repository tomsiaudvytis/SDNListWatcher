using System;
using System.IO;
using System.Xml.Serialization;
using Common.Interfaces;

namespace SdnListWatcher
{
    public class XmlParser : IXmlParser
    {
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