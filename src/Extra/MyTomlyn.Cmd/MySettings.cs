using Tomlyn.Model;

namespace MyTomlyn.Cmd;

public class MySettings
{
    public string DevDb { get; set; } = string.Empty;
    public string ProdDb { get; set; } = string.Empty;
    public string DevDbServer { get; set; } = string.Empty;
    public string ProdDbServer { get; set; } = string.Empty;
    public string SqlBackupDirectory { get; set; } = string.Empty;
    public string SqlBackupShare { get; set; } = string.Empty;
    public string BackupDevFile { get; set; } = string.Empty;
    public string BackupProdFile { get; set; } = string.Empty;
    public string DevDbFile { get; set; } = string.Empty;
    public string DevLogFile { get; set; } = string.Empty;
    
    // Dictionary for dynamic host configurations
    public Dictionary<string, HostModel> Hosts { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Creates MySettings from a TomlTable, extracting host tables into the Hosts dictionary
    /// </summary>
    public static MySettings FromTomlTable(TomlTable table)
    {
        var settings = new MySettings();
        
        // Map root-level string properties
        if (table.TryGetValue("dev_db", out var devDb)) settings.DevDb = (string)devDb;
        if (table.TryGetValue("prod_db", out var prodDb)) settings.ProdDb = (string)prodDb;
        if (table.TryGetValue("dev_db_server", out var devDbServer)) settings.DevDbServer = (string)devDbServer;
        if (table.TryGetValue("prod_db_server", out var prodDbServer)) settings.ProdDbServer = (string)prodDbServer;
        if (table.TryGetValue("sql_backup_directory", out var sqlBackupDirectory)) settings.SqlBackupDirectory = (string)sqlBackupDirectory;
        if (table.TryGetValue("sql_backup_share", out var sqlBackupShare)) settings.SqlBackupShare = (string)sqlBackupShare;
        if (table.TryGetValue("backup_dev_file", out var backupDevFile)) settings.BackupDevFile = (string)backupDevFile;
        if (table.TryGetValue("backup_prod_file", out var backupProdFile)) settings.BackupProdFile = (string)backupProdFile;
        if (table.TryGetValue("dev_db_file", out var devDbFile)) settings.DevDbFile = (string)devDbFile;
        if (table.TryGetValue("dev_log_file", out var devLogFile)) settings.DevLogFile = (string)devLogFile;

        // Extract all table sections as hosts (any key that maps to a TomlTable)
        foreach (var key in table.Keys)
        {
            if (table[key] is TomlTable hostTable)
            {
                var hostModel = new HostModel();
                
                if (hostTable.TryGetValue("one_drive_folder", out var oneDriveFolder))
                    hostModel.OneDriveFolder = (string)oneDriveFolder;
                
                if (hostTable.TryGetValue("one_drive_latest_version_folder", out var oneDriveLatestVersionFolder))
                    hostModel.OneDriveLatestVersionFolder = (string)oneDriveLatestVersionFolder;
                
                settings.Hosts[key] = hostModel;
            }
        }

        return settings;
    }
}

public class HostModel
{
    public string OneDriveFolder { get; set; } = string.Empty;
    public string OneDriveLatestVersionFolder { get; set; } = string.Empty;
}