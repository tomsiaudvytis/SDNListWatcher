using System;

namespace Common.Interfaces
{
    public interface IXmlParser
    {
        bool TryDeserializeObject<T>(out T result, string content);
    }
}