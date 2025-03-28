using AutoRenovatioNS.Interfaces;

namespace AutoRenovatioNS.Models;

public class DefaultUpdater : IUpdater
{
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
start """" ""{appInfo.ExecutablePath}""
";
    }
}