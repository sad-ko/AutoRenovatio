using AutoRenovatioNS.Models;

namespace AutoRenovatioNS.Interfaces;

/// <summary>
/// <b>Auto Renovatio</b> runs a script to install updates, the logic of those scripts should be implemented by the user,
/// but this library includes a simple example of how to make one: <see cref="DefaultUpdater"/>.
/// </summary>
/// <summary xml:lang="es">
/// <b>Auto Renovatio</b> ejecuta un script para instalar las actualizaciones,la logica de esos scripts deberia ser implementada por el usuario,
/// pero esta biblioteca incluye un ejemplo de como generar uno: <see cref="DefaultUpdater"/>.
/// </summary>
public interface IUpdater
{
    public string GetLinuxScript(string path, string file, ApplicationInfo appInfo);

    public string GetWindowsScript(string path, string file, ApplicationInfo appInfo);
}