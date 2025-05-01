namespace BibleWell.App;

/// <summary>
/// For configuration to work correctly, all appsettings.{environment}.json files must use one of these environments
/// and the name of the enum value must match the environment name.
/// </summary>
public enum AppEnvironment
{
    Development, // AKA Local
    Dev,
    QA,
    Production, // AKA Prod
}