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
    public Dictionary<string, HostModel> Hosts { get; set; } = new();
}

public class HostModel
{
    public string OneDriveFolder { get; set; } = string.Empty;
    public string OneDriveLatestVersionFolder { get; set; } = string.Empty;
}