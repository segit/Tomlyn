using Tomlyn;
using Tomlyn.Model;
using MyTomlyn.Cmd;

// Read the TOML file
var tomlContent = File.ReadAllText("Resources/appSettings.toml");

// Parse to TomlTable using Toml.ToModel<T> and convert to MySettings
var options = new TomlModelOptions { IgnoreMissingProperties = true };
var mySettings = Toml.ToModel<MySettings>(tomlContent, options: options);

var mySettings2 = Toml.ToModel<Dictionary<string, HostModel>>(tomlContent, options: options);


var tomlTable = Toml.ToModel<TomlTable>(tomlContent);
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
