using Tomlyn;
using Tomlyn.Model;
using MyTomlyn.Cmd;

// Read the TOML file
var tomlContent = File.ReadAllText("Resources/appSettings.toml");

// Parse to TomlTable first
var tomlTable = Toml.ToModel(tomlContent);

// Create settings instance
var settings = new MySettings();

// Deserialize root-level properties
if (tomlTable.TryGetValue("dev_db", out var devDb)) settings.DevDb = (string)devDb;
if (tomlTable.TryGetValue("prod_db", out var prodDb)) settings.ProdDb = (string)prodDb;
if (tomlTable.TryGetValue("dev_db_server", out var devDbServer)) settings.DevDbServer = (string)devDbServer;
if (tomlTable.TryGetValue("prod_db_server", out var prodDbServer)) settings.ProdDbServer = (string)prodDbServer;
if (tomlTable.TryGetValue("sql_backup_directory", out var sqlBackupDirectory)) settings.SqlBackupDirectory = (string)sqlBackupDirectory;
if (tomlTable.TryGetValue("sql_backup_share", out var sqlBackupShare)) settings.SqlBackupShare = (string)sqlBackupShare;
if (tomlTable.TryGetValue("backup_dev_file", out var backupDevFile)) settings.BackupDevFile = (string)backupDevFile;
if (tomlTable.TryGetValue("backup_prod_file", out var backupProdFile)) settings.BackupProdFile = (string)backupProdFile;
if (tomlTable.TryGetValue("dev_db_file", out var devDbFile)) settings.DevDbFile = (string)devDbFile;
if (tomlTable.TryGetValue("dev_log_file", out var devLogFile)) settings.DevLogFile = (string)devLogFile;

// Deserialize host configurations into Dictionary
foreach (var key in tomlTable.Keys)
{
    if (tomlTable[key] is TomlTable hostTable && (key.StartsWith("host") || key.Contains("host")))
    {
        var hostModel = new HostModel();
        
        if (hostTable.TryGetValue("oneDrive_folder", out var oneDriveFolder))
            hostModel.OneDriveFolder = (string)oneDriveFolder;
        
        if (hostTable.TryGetValue("oneDrive_latest_version_folder", out var oneDriveLatestVersionFolder))
            hostModel.OneDriveLatestVersionFolder = (string)oneDriveLatestVersionFolder;
        
        settings.Hosts[key] = hostModel;
    }
}

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
