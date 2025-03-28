using AutoRenovatioNS.Models;
using AutoRenovatioNS.Parsers;
using Version = AutoRenovatioNS.Models.Version;

namespace AutoRenovatio_Tests;

public class TestComparasion()
{
    [Fact]
    public async Task Test()
    {
        var current = new DefaultUpdate();

        var content = await File.ReadAllTextAsync(@"./dummy.xml");
        var information = new XmlParser<DefaultUpdate>().Parse(content);

        Assert.True(information is not null && current.IsNewer(information));

        var newerCurrent = new DefaultUpdate()
        {
            Version = new Version("5.0.1")
        };
        Assert.False(information is not null && newerCurrent.IsNewer(information));
    }
}

public class TestParserDefault()
{
    [Fact]
    public async Task Test()
    {
        var content = await File.ReadAllTextAsync(@"./dummy.xml");
        var information = new XmlParser<DefaultUpdate>().Parse(content);

        Assert.NotNull(information);
        Assert.Equal("3.2.1.beta", information.Version.ToString());
        Assert.Equal("https://github.com/runfo-sa/canelary", information.Url);
        Assert.Equal("Tested by XML parser", information.Changelog);
        Assert.False(information.Mandatory);
    }
}

public class TestParserYaml()
{
    [Fact]
    public async Task Test()
    {
        var content = await File.ReadAllTextAsync(@"./dummy.yaml");
        var information = new YamlParser<DefaultUpdate>().Parse(content);

        Assert.NotNull(information);
        Assert.Equal("3.2.1.beta", information.Version.ToString());
        Assert.Equal("https://github.com/runfo-sa/canelary", information.Url);
        Assert.Equal("Tested by YAML parser", information.Changelog);
        Assert.True(information.Mandatory);
    }
}

public class TestParserJson()
{
    [Fact]
    public async Task Test()
    {
        var content = await File.ReadAllTextAsync(@"./dummy.json");
        var information = new JsonParser<DefaultUpdate>().Parse(content);

        Assert.NotNull(information);
        Assert.Equal("3.2.1.beta", information.Version.ToString());
        Assert.Equal("https://github.com/runfo-sa/canelary", information.Url);
        Assert.Equal("Tested by JSON parser", information.Changelog);
        Assert.True(information.Mandatory);
    }
}