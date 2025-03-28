using AutoRenovatioNS.Interfaces;
using System.Xml.Serialization;

namespace AutoRenovatioNS.Parsers;

public class XmlParser<T> : IParser<T>
     where T : class, IUpdateInfo, new()
{
    public T? Parse(string content)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(content);
        var info = serializer.Deserialize(reader) as T;
        return info;
    }
}