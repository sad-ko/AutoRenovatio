using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AutoRenovatio;

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

    private void Parse(string ver, char deilimiter = '.')
    {
        var parts = ver.Split(deilimiter);

        if (parts.Length > 4)
        {
            throw new ArgumentOutOfRangeException(ver, "Cadena de versionado supera los 4 parametros requeridos por la clase; Major.Minor.Build.Suffix");
        }

        Major = int.Parse(parts.ElementAtOrDefault(0) ?? "0");
        Minor = int.Parse(parts.ElementAtOrDefault(1) ?? "0");
        Build = int.Parse(parts.ElementAtOrDefault(2) ?? "0");
        Suffix = parts.ElementAtOrDefault(3);
    }

    public override String ToString()
    {
        return (Suffix != null) ? $"{Major}.{Minor}.{Build}.{Suffix}" : $"{Major}.{Minor}.{Build}";
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
            Parse(content);
            reader.ReadEndElement();
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteString(ToString());
    }
}