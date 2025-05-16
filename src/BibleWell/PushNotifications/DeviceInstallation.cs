namespace BibleWell.PushNotifications;

public class DeviceInstallation
{
    public required string InstallationId { get; set; }
    
    public required string Platform { get; set; }
    
    public required string PushChannel { get; set; }
    
    public List<string> Tags { get; set; } = [];
}