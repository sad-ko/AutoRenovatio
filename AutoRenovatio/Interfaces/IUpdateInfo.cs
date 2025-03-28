using AutoRenovatioNS.Models;

namespace AutoRenovatioNS.Interfaces;

/// <summary>
/// Interface that defines all the methods needed for an <b>UpdateInfo</b> file.
/// <br/>
/// An <b>UpdateInfo</b> file should contain all the necessary information to make an update.
/// <br/><br/>
/// See <see cref="DefaultUpdate"/> for an example.
/// </summary>
/// <summary xml:lang="es">
/// Interfaz que define todos los metodos necesarios para un archivo <b>UpdateInfo</b>.
/// <br/>
/// <b>UpdateInfo</b> es un archivo que debe contener toda la informacion necesaria para realizar una actualizacion.
/// <br/><br/>
/// Ejemplo: <see cref="DefaultUpdate"/>.
/// </summary>
public interface IUpdateInfo
{
    public bool IsNewer(IUpdateInfo info);

    public string GetUrl();

    public string GetVersion();
}