namespace AutoRenovatio;

public class DefaultUpdate : IUpdate
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

    public bool IsNewer(IUpdate info)
    {
        return info is DefaultUpdate i && i.Version > Version;
    }

    public string GetUrl()
    {
        return Url ?? throw new MissingMemberException("No se encuentra url de descarga.");
    }

    public string GetVersion()
    {
        return Version.ToString();
    }

    public String GetLinuxScript(string path, string file, ApplicationInfo appInfo)
    {
        throw new NotImplementedException();
    }

    public String GetWindowsScript(string path, string file, ApplicationInfo appInfo)
    {
        return $@"
@echo off
:: Espera 3 segundos antes de comenzar la instalacion,
:: para asegurarnos que no quedara ningun proceso ejecutandose
timeout /t 3 /nobreak > NUL

:: Asumimos que el archivo de instalacion es un .zip
rmdir /s /q ""{path}/extracted""
mkdir ""{path}/extracted""
tar -xf ""{file}"" -C ""{path}/extracted""

:: Copias los nuevos archivos
xcopy /s /y ""{path}/extracted\*"" ""{appInfo.RootPath}""

:: Ejecutamos nuevamente la aplicacion
start /d ""{appInfo.ExecutablePath}""

:: Eliminamos el script
del ""%~f0""
";
    }
}