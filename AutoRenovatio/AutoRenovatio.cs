using AutoRenovatioNS.Interfaces;
using AutoRenovatioNS.Models;
using AutoRenovatioNS.Parsers;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoRenovatioNS;

/// <summary>
/// <b>Auto Renovatio</b> downloads an <b>UpdateInfo</b> file from the URL provided by <paramref name="informationUrl"/>.
/// <br/>
/// Compares the file with the <paramref name="currentVersion"/> using the <see cref="CheckForUpdatesAsync{P, T}()"/> function,
/// <br/>
/// and in the case that it's newer can be updated with the <see cref="DownloadUpdateAsync{T, U}(T)"/> function.
/// </summary>
/// <summary xml:lang="es">
/// <b>Auto Renovatio</b> descarga un archivo <b>UpdateInfo</b> proveniente de la URL en <paramref name="informationUrl"/>.
/// <br/>
/// Compara el archivo con <paramref name="currentVersion"/> usando la funcion <see cref="CheckForUpdatesAsync{P, T}()"/>,
/// <br/>
/// y en caso de ser mas nueva puede ser actualizada utilizando la funcion <see cref="DownloadUpdateAsync{T, U}(T)"/>.
/// </summary>
public class AutoRenovatio(ApplicationInfo appInfo, IUpdateInfo currentVersion, string informationUrl)
{
    public ApplicationInfo AppInfo { get; private set; } = appInfo;
    public IUpdateInfo CurrentVersion { get; private set; } = currentVersion;
    public string InformationFileUrl { get; private set; } = informationUrl;

    private readonly HttpClient _client = new();

    /// <summary>
    /// Uses the default classes.
    /// </summary>
    /// <summary xml:lang="es">
    /// Utiliza las clases por default.
    /// </summary>
    public async Task<DefaultUpdate?> CheckForUpdatesAsync()
    {
        return await CheckForUpdatesAsync<XmlParser<DefaultUpdate>, DefaultUpdate>();
    }

    /// <summary>
    /// Checks if there is a new update pending with the <see cref="InformationFileUrl"/> URL.
    /// <br/>
    /// Returns an <see cref="IUpdateInfo"/> object in case there is an update,
    /// otherwise returns <see langword="null"/>.
    /// </summary>
    /// <summary xml:lang="es">
    /// Revisa si hay alguna actualizacion pendiente con la URL de <see cref="InformationFileUrl"/>.
    /// <br/>
    /// Devuelve un objeto del tipo <see cref="IUpdateInfo"/> si hay una actualizacion,
    /// en caso contrario devuelve <see langword="null"/>.
    /// </summary>
    public async Task<T?> CheckForUpdatesAsync<P, T>()
        where P : IParser<T>, new()
        where T : class, IUpdateInfo, new()
    {
        var content = await _client.GetStringAsync(InformationFileUrl);
        var info = new P().Parse(content);

        return (info is not null && CurrentVersion.IsNewer(info)) ? info : null;
    }

    /// <summary>
    /// Uses the default classes.
    /// </summary>
    /// <summary xml:lang="es">
    /// Utiliza las clases por default.
    /// </summary>
    public async Task DownloadUpdateAsync(DefaultUpdate info)
    {
        await DownloadUpdateAsync<DefaultUpdate, DefaultUpdater>(info);
    }

    /// <summary>
    /// Downloads the update and runs the updating script defined by the <see cref="IUpdater"/> implementation.
    /// <br/>
    /// <b>WARNING!</b> this function WILL close this process and all other process of the same application,
    /// please make sure to do a pre-exit clean-up before calling it.
    /// </summary>
    /// <summary xml:lang="es">
    /// Descarga la actualización y ejecuta el script de actualización definido por la implementación de <see cref="IUpdater"/>.
    /// <br/>
    /// <b>AVISO!</b> esta función CERRARÁ este proceso y todos los demás procesos de la misma aplicación,
    /// por favor asegúrese de hacer una limpieza previa a la salida antes de llamarla.
    /// </summary>
    public async Task DownloadUpdateAsync<T, U>(T info)
        where T : IUpdateInfo, new()
        where U : IUpdater, new()
    {
        var downloadPath = Path.Combine(Path.GetTempPath(), AppInfo.Name);
        Directory.CreateDirectory(downloadPath);

        var bytes = await _client.GetByteArrayAsync(info.GetUrl());

        var filename = Path.Combine(downloadPath, $"{AppInfo.Name}-{info.GetVersion()}");
        await File.WriteAllBytesAsync(filename, bytes);

        var isWin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        var ext = isWin ? "bat" : "sh";
        var scriptname = Path.Combine(downloadPath, $"update.{ext}");

        var updater = new U();
        var script = isWin ?
            updater.GetWindowsScript(downloadPath, filename, AppInfo)
            : updater.GetLinuxScript(downloadPath, filename, AppInfo);
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

    /// <summary>
    /// Closes all instances of this application.
    /// </summary>
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
                // Ignore
            }
        }
    }
}