# Auto Renovatio

[![en](https://img.shields.io/badge/lang-en-blue.svg)](./README.md)

Auto Renovatio es un actualizador simple y altamente personalizable.

## Ejemplo

Puedes ponerlo en marcha con unas pocas líneas de código!

```C#
var updater = new AutoRenovatio(
    new ApplicationInfo("AutoRenovatio_Example"),
    new DefaultUpdate("1.0.0"),
    "https://github.com/sad-ko/AutoRenovatio/releases/download/test/UpdateInfo.xml"
);

var update = await updater.CheckForUpdatesAsync();
if (update != null)
{
    await updater.DownloadUpdateAsync(update);
}
else
{
    Console.WriteLine("Already updated!");
}
```

Consulte el [Ejemplo](https://github.com/sad-ko/AutoRenovatio/tree/main/AutoRenovatio-Example) para obtener una versión más detallada.

## ¿Como funciona?

Auto Renovatio descargará primero un archivo **UpdateInfo**, este archivo debería contener toda la información necesaria para realizar una actualización de la aplicación.

Ejemplo de un archivo **UpdateInfo**:

```xml
<?xml version="1.0" encoding="utf-8"?>
<UpdateInfo>
	<Version>3.2.1.beta</Version>
	<Url>https://github.com/runfo-sa/canelary</Url>
	<Changelog>Tested by XML parser</Changelog>
	<Mandatory>false</Mandatory>
</UpdateInfo>
```

El formato del archivo puede ser el que necesites, Auto Renovatio ofrece suficiente abstracción para que puedas crear tu propio _parser_ para estos archivos.

Por defecto ya incluye _parsers_ para XML, JSON y YAML (utilizando [YamlDotNet](https://github.com/aaubry/YamlDotNet)).

Una vez que haya una actualización disponible, Auto Renovatio la descargará en una carpeta temporal y ejecutará un script con las instrucciones necesarias para realizar la instalación.

Este script de instalación también debe ser definido por el usuario, por defecto ofrecemos un simple script batch/bash que descomprime el archivo descargado y copia su contenido a la carpeta raíz de la aplicación.
