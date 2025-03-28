namespace AutoRenovatioNS.Models;

/// <summary>
/// Defines all the information of your application needed for <b>Auto Renovatio</b>.
/// <br/><br/>
/// <paramref name="Name"/>: The name of your application.<br/>
/// <paramref name="RootPath"/>: The full path to the root folder of your program.<br/>
/// <paramref name="ExecutablePath"/>: The full path to the executable of your program.
/// </summary>
/// <summary xml:lang="es">
/// Define toda la informacion sobre tu aplicacion necesaria para <b>Auto Renovatio</b>.
/// <br/><br/>
/// <paramref name="Name"/>: Nombre de tu aplicacion.<br/>
/// <paramref name="RootPath"/>: Ruta absoluta de la carpeta raiz de tu programa.<br/>
/// <paramref name="ExecutablePath"/>: Ruta absoluta del ejecutable de tu programa.
/// </summary>
public readonly record struct ApplicationInfo(
    string Name,
    string RootPath,
    string ExecutablePath
)
{
    /// <summary>
    /// Defines all the information of your application needed for <b>Auto Renovatio</b>.
    /// <br/><br/>
    /// <paramref name="Name"/>: The name of your application.<br/>
    /// <paramref name="RootPath"/>: The full path to the root folder of your program.<br/>
    /// <paramref name="ExecutablePath"/>: The full path to the executable of your program.
    /// </summary>
    /// <summary xml:lang="es">
    /// Define toda la informacion sobre tu aplicacion necesaria para <b>Auto Renovatio</b>.
    /// <br/><br/>
    /// <paramref name="Name"/>: Nombre de tu aplicacion.<br/>
    /// <paramref name="RootPath"/>: Ruta absoluta de la carpeta raiz de tu programa.<br/>
    /// <paramref name="ExecutablePath"/>: Ruta absoluta del ejecutable de tu programa.
    /// </summary>
    public ApplicationInfo(string name) : this(name, AppDomain.CurrentDomain.BaseDirectory, Environment.ProcessPath!) { }
}