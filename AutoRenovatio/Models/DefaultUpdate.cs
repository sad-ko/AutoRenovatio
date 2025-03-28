using AutoRenovatioNS.Interfaces;
using System.Xml.Serialization;

namespace AutoRenovatioNS.Models;

[XmlRoot("UpdateInfo")]
public class DefaultUpdate : IUpdateInfo
{
    public Version Version { get; set; } = new Version("0.0.0");
    public bool Mandatory { get; set; } = false;
    public string? Url { get; set; }
    public string? Changelog { get; set; }

    public DefaultUpdate()
    {
    }

    public DefaultUpdate(string version)
    {
        Version = new Version(version);
    }

    public bool IsNewer(IUpdateInfo info)
    {
        return info is DefaultUpdate i && i.Version > Version;
    }

    public string GetUrl()
    {
        return Url ?? throw new MissingMemberException("No URL found.");
    }

    public string GetVersion()
    {
        return Version.ToString();
    }
}