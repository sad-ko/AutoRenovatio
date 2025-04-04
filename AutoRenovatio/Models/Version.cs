using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AutoRenovatioNS.Models;

/// <summary>
/// <see cref="Version"/> is an example on how you can make a versioning class,
/// it's used by the default <b>UpdateInfo</b> class, <see cref="DefaultUpdate"/>.
/// But it can easily be replaced by a versioning class of your need.
/// </summary>
/// <summary xml:lang="es">
/// <see cref="Version"/> es un ejemplo de como crear una clase de versionado,
/// es utilizada para la clase default de <b>UpdateInfo</b>, <see cref="DefaultUpdate"/>.
/// Pero se podria facilmente crear una clase de versionado para tus necesidades.
/// </summary>
public class Version : IXmlSerializable
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Build { get; set; }
    public string? Suffix { get; set; }

    public static bool operator <(Version a, Version b)
    {
        return (a.Major < b.Major)
            || (a.Major == b.Major && a.Minor < b.Minor)
            || (a.Major == b.Major && a.Minor == b.Minor && a.Build < b.Build)
            || (a.Major == b.Major && a.Minor == b.Minor && a.Build == b.Build && a.Suffix != b.Suffix);
    }

    public static bool operator >(Version a, Version b)
    {
        return (a.Major > b.Major)
            || (a.Major == b.Major && a.Minor > b.Minor)
            || (a.Major == b.Major && a.Minor == b.Minor && a.Build > b.Build)
            || (a.Major == b.Major && a.Minor == b.Minor && a.Build == b.Build && a.Suffix != b.Suffix);
    }

    public Version()
    {
    }

    public Version(string ver, char deilimiter = '.')
    {
        Parse(ver, deilimiter);
    }

    public override String ToString()
    {
        return (Suffix != null) ? $"{Major}.{Minor}.{Build}-{Suffix}" : $"{Major}.{Minor}.{Build}";
    }

    protected void Parse(string ver, char deilimiter = '.')
    {
        var parts = ver.Split(deilimiter);

        if (parts.Length > 3)
        {
            throw new ArgumentOutOfRangeException(ver, "String exceeds the 3 parameters required by the class: Major.Minor.Build");
        }

        var build_suffix = (parts.ElementAtOrDefault(2) ?? "0").Split('-');

        Major = int.Parse(parts.ElementAtOrDefault(0) ?? "0");
        Minor = int.Parse(parts.ElementAtOrDefault(1) ?? "0");
        Build = int.Parse(build_suffix[0]);
        Suffix = build_suffix.ElementAtOrDefault(1);
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        reader.MoveToContent();
        reader.ReadStartElement();
        if (!reader.IsEmptyElement)
        {
            var content = reader.ReadContentAsString();
            this.Parse(content);
            reader.ReadEndElement();
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteString(ToString());
    }
}