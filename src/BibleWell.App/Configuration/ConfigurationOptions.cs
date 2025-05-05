namespace BibleWell.App.Configuration;

public sealed class ConfigurationOptions
{
    public required ApplicationInsightsOptions ApplicationInsights { get; init; }
    public required string AquiferApiBaseUri { get; init; }
    public required string AquiferApiKey { get; init; }
    public required AppEnvironment Environment { get; init; }
}

public sealed class ApplicationInsightsOptions
{
    public required string ConnectionString { get; init; }
}