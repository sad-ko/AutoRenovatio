using System.Text.Json;
using System.Text.Json.Serialization;
using Version = AutoRenovatioNS.Models.Version;

namespace AutoRenovatioNS.Converters;

public class VersionJsonConverter : JsonConverter<Version>
{
    public override Version Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var versionString = reader.GetString();
        return new Version(versionString!);
    }

    public override void Write(Utf8JsonWriter writer, Version value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}