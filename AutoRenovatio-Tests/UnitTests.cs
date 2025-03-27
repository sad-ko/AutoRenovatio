using AutoRenovatio;
using Version = AutoRenovatio.Version;

namespace AutoRenovatio_Tests;

public class TestParserDefault()
{
    [Fact]
    public async Task Test()
    {
        var content = await File.ReadAllTextAsync(@"./dummy.xml");
        var information = new XmlParser<DefaultUpdate>().Parse(content);

        Assert.NotNull(information);
        Assert.Equal("2.0.0.rc", information.Version.ToString());
        Assert.Equal("https://github.com/runfo-sa/canelary", information.Url);
        Assert.Equal("Cambio todo", information.Changelog);
        Assert.False(information.Mandatory);
    }
}

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
            Version = new Version("2.0.1")
        };
        Assert.False(information is not null && newerCurrent.IsNewer(information));
    }
}