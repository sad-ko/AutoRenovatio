# Auto Renovatio

[![es](https://img.shields.io/badge/lang-es-green.svg)](./README.es.md)

Auto Renovatio it's a very simple and highly customizable auto-updater.

## Example

You can get it running with just a very few lines of code!

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

See the [Example](https://github.com/sad-ko/AutoRenovatio/tree/main/AutoRenovatio-Example) for a more detailed version.

## How does it work?

Auto Renovatio will first download an **UpdateInfo** file, this file should contain all the necessary information needed to make an application update.

**UpdateInfo** file example:

```xml
<?xml version="1.0" encoding="utf-8"?>
<UpdateInfo>
	<Version>3.2.1.beta</Version>
	<Url>https://github.com/runfo-sa/canelary</Url>
	<Changelog>Tested by XML parser</Changelog>
	<Mandatory>false</Mandatory>
</UpdateInfo>
```

The format of the file can be whatever you need, Auto Renovatio offers enough abstraction to let you create your own parser for these files.

By default it already includes parsers for XML, JSON and YAML (using [YamlDotNet](https://github.com/aaubry/YamlDotNet)).

Once an update is available, Auto Renovatio will download it to a temporary folder and execute a script with the instructions needed to make the installation.

This installation script should also be defined by the user, by default we offer a simple batch/bash script that unzip the downloaded file and copy it's content to the application root folder.
