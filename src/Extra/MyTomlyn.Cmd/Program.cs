using Tomlyn;
using MyTomlyn.Cmd;

// Read the TOML file
var tomlContent = File.ReadAllText("Resources/appSettings.toml");

// Parse to TomlTable using Toml.ToModel<T> and convert to MySettings

/*
 *Tomlyn.TomlException
  HResult=0x80131500
  Message=(12,2) : error : Unable to set the property host1 on object type MyTomlyn.Cmd.MySettings.
(16,2) : error : Unable to set the property host2 on object type MyTomlyn.Cmd.MySettings.

  Source=Tomlyn
  StackTrace:
   at Tomlyn.Toml.ToModel[T](DocumentSyntax syntax, TomlModelOptions options) in D:\esv\src\github\xoofx\Tomlyn\src\Tomlyn\Toml.cs:line 207
   at Tomlyn.Toml.ToModel[T](String text, String sourcePath, TomlModelOptions options) in D:\esv\src\github\xoofx\Tomlyn\src\Tomlyn\Toml.cs:line 158
   at Program.<Main>$(String[] args) in D:\esv\src\github\xoofx\Tomlyn\src\Extra\MyTomlyn.Cmd\Program.cs:line 8
 
 */
var mySettings = Toml.ToModel<MySettings>(tomlContent);


var tomlTable = Toml.ToModel<Tomlyn.Model.TomlTable>(tomlContent);
var settings = MySettings.FromTomlTable(tomlTable);

// Demonstrate accessing settings
Console.WriteLine($"Dev DB: {settings.DevDb}");
Console.WriteLine($"Prod DB: {settings.ProdDb}");
Console.WriteLine($"SQL Backup Directory: {settings.SqlBackupDirectory}");

// Accessing hosts dynamically by name
if (settings.Hosts.TryGetValue("host1", out var host1))
{
    Console.WriteLine($"\nHost1 - OneDrive Folder: {host1.OneDriveFolder}");
    Console.WriteLine($"Host1 - OneDrive Latest Version Folder: {host1.OneDriveLatestVersionFolder}");
}

if (settings.Hosts.TryGetValue("host2", out var host2))
{
    Console.WriteLine($"\nHost2 - OneDrive Folder: {host2.OneDriveFolder}");
    Console.WriteLine($"Host2 - OneDrive Latest Version Folder: {host2.OneDriveLatestVersionFolder}");
}

// Demonstrate runtime host access
var hostName = "host1"; // This could come from runtime configuration
if (settings.Hosts.TryGetValue(hostName, out var dynamicHost))
{
    Console.WriteLine($"\nDynamic access to {hostName}: {dynamicHost.OneDriveFolder}");
}
