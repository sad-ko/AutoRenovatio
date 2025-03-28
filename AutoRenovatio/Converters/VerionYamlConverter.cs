using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace AutoRenovatioNS.Converters;

public class VerionYamlConverter : IYamlTypeConverter
{
    public Boolean Accepts(Type type)
    {
        return type == typeof(Models.Version);
    }

    public Object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    {
        if (parser.TryConsume<Scalar>(out Scalar? ver))
        {
            return new Models.Version(ver.Value);
        }

        throw new InvalidOperationException("The class AutoRenovatio.Version expected a YAML Scalar");
    }

    public void WriteYaml(IEmitter emitter, Object? value, Type type, ObjectSerializer serializer)
    {
        var version = value as Models.Version;
        emitter.Emit(new Scalar("Version", version!.ToString()));
    }
}