using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibleWell.Utility;

public static class JsonUtilities
{
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public static T DefaultDeserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions)!;
    }
}