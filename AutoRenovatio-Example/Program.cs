using AutoRenovatio;
using System.Reflection;

namespace AutoRenovatio_Example;

internal static class Program
{
    public static string Version => Assembly.GetExecutingAssembly()
        .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
        .Select(x => x.InformationalVersion)
        .First();

    private static async Task Main(string[] args)
    {
        Console.WriteLine($"Current Version: {Version}");

        var appInfo = new ApplicationInfo("AutoRenovatio_Example", AppDomain.CurrentDomain.BaseDirectory, Environment.ProcessPath!);
        var currentVersion = new DefaultUpdate(Version);
        var updater = new AutoRenovatio.AutoRenovatio(appInfo, currentVersion, "");

        //await updater.CheckForUpdatesAsync();
    }
}