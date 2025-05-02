namespace BibleWell;

public interface IApplicationInfoService
{
    string BuildNumber { get; }
    string Version { get; }
}