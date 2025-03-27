using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoRenovatio;

public class AutoRenovatio(ApplicationInfo appInfo, IUpdate currentVersion, string informationUrl)
{
    public ApplicationInfo AppInfo { get; private set; } = appInfo;
    public IUpdate CurrentVersion { get; private set; } = currentVersion;
    public string InformationFileUrl { get; private set; } = informationUrl;

    private readonly HttpClient _client = new();

    public async Task CheckForUpdatesAsync()
    {
        await CheckForUpdatesAsync<XmlParser<DefaultUpdate>, DefaultUpdate>();
    }

    public async Task CheckForUpdatesAsync<P, T>()
        where P : IParser<T>, new()
        where T : IUpdate, new()
    {
        try
        {
            var content = await _client.GetStringAsync(InformationFileUrl);
            var info = new P().Parse(content);

            if (info is not null && CurrentVersion.IsNewer(info))
            {
                await DownloadUpdate(info);
            }
        }
        catch (Exception)
        {
            // TODO! Error handling
        }
    }

    private async Task DownloadUpdate<T>(T info)
        where T : IUpdate, new()
    {
        var downloadPath = Path.Combine(Path.GetTempPath(), AppInfo.Name);
        Directory.CreateDirectory(downloadPath);

        var bytes = await _client.GetByteArrayAsync(info.GetUrl());

        var filename = Path.Combine(downloadPath, $"{AppInfo}-{info.GetVersion()}");
        await File.WriteAllBytesAsync(filename, bytes);

        var isWin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        var ext = isWin ? "bat" : "sh";
        var scriptname = Path.Combine(downloadPath, $"update.{ext}");
        var script = isWin ?
            info.GetWindowsScript(downloadPath, filename, AppInfo)
            : info.GetLinuxScript(downloadPath, filename, AppInfo);
        await File.WriteAllTextAsync(scriptname, script);

        CloseInstances();

        Process.Start(new ProcessStartInfo
        {
            FileName = scriptname,
            UseShellExecute = true,
            CreateNoWindow = true
        });

        Environment.Exit(0);
    }

    private static void CloseInstances()
    {
        var currentProcess = Process.GetCurrentProcess();
        foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
        {
            try
            {
                string? processPath = process.MainModule?.FileName;

                // Get all instances of assembly except current
                if (process.Id == currentProcess.Id || currentProcess.MainModule?.FileName != processPath)
                {
                    continue;
                }

                if (process.CloseMainWindow())
                {
                    process.WaitForExit((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
                }

                if (process.HasExited)
                {
                    continue;
                }

                process.Kill();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}