namespace BibleWell.App.Configuration;

public sealed class ConfigurationOptions
{
    public required string AquiferApiBaseUri { get; init; }
    public required string AquiferApiKey { get; init; }
}
