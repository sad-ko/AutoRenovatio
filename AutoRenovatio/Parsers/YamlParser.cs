using AutoRenovatioNS.Converters;
using AutoRenovatioNS.Interfaces;
using YamlDotNet.Serialization;

namespace AutoRenovatioNS.Parsers;

public class YamlParser<T> : IParser<T>
    where T : class, IUpdateInfo, new()
{
    public T? Parse(String content)
    {
        var deserializer = new DeserializerBuilder()
            .WithTypeConverter(new VerionYamlConverter())
            .Build();
        return deserializer.Deserialize<T>(content);
    }
}