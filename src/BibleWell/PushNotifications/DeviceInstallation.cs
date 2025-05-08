using System.Text.Json.Serialization;

namespace BibleWell.PushNotifications;

public class DeviceInstallation
{
    [JsonPropertyName("installationId")]
    public required string InstallationId { get; set; } // or nullable?

    [JsonPropertyName("platform")]
    public required string Platform { get; set; } // or nullable?

    [JsonPropertyName("pushChannel")]
    public required string PushChannel { get; set; } // or nullable?

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = [];
}