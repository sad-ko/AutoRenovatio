using AutoRenovatioNS;
using AutoRenovatioNS.Models;
using System.Reflection;

namespace AutoRenovatio_Example;

/// <summary>
/// This is a simple example on how you can utilize this library.
/// </summary>
/// <summary xml:lang="es">
/// Este es un ejemplo sencillo de cómo se puede utilizar esta biblioteca.
/// </summary>
internal static class Program
{
    private static string Version => Assembly.GetExecutingAssembly()
        .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
        .Select(x => x.InformationalVersion)
        .First();

    private static async Task Main(string[] args)
    {
        Console.WriteLine($"Current Version: {Version}\r\n");

        var updater = new AutoRenovatio(
            new ApplicationInfo("AutoRenovatio_Example"),
            new DefaultUpdate(Version),
            // In production these would be .../releases/latest/download/UpdateInfo.xml
            "https://github.com/sad-ko/AutoRenovatio/releases/download/test/UpdateInfo.xml"
        );

        var update = await updater.CheckForUpdatesAsync();
        if (update != null)
        {
            Console.WriteLine("Update Pending!");
            Console.WriteLine($"New Version: {update.Version}");
            Console.WriteLine($"Changelog: {update.Changelog}");
            Console.WriteLine($"Mandatory?: {update.Mandatory}");
            Console.WriteLine("\r\nDownload update? [Y/n]");

            var key = Console.ReadKey(true);
            if (key.KeyChar == 'y' || key.KeyChar == 'Y' || key.Key == ConsoleKey.Enter)
            {
                // Do program exiting clean-up before calling this function...
                await updater.DownloadUpdateAsync(update);
            }
        }
        else
        {
            Console.WriteLine("Already updated!");
        }

        Console.WriteLine("\r\nPress any key to end...");
        Console.ReadKey();
    }
}