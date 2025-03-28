using AutoRenovatioNS.Converters;
using AutoRenovatioNS.Interfaces;
using System.Text.Json;

namespace AutoRenovatioNS.Parsers;

public class JsonParser<T> : IParser<T>
    where T : class, IUpdateInfo, new()
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new VersionJsonConverter() },
        WriteIndented = true
    };

    public T? Parse(String content)
    {
        return JsonSerializer.Deserialize<T>(content, _options);
    }
}