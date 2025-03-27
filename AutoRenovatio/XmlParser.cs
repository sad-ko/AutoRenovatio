using System.Xml.Serialization;

namespace AutoRenovatio;

public class XmlParser<T> : IParser<T>
     where T : class, IUpdate, new()
{
    public T? Parse(string content)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var reader = new StringReader(content);
        var info = serializer.Deserialize(reader) as T;
        return info;
    }
}