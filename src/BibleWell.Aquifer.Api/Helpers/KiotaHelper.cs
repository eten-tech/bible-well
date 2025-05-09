using BibleWell.Utility;
using Microsoft.Kiota.Abstractions.Serialization;

namespace BibleWell.Aquifer.Api.Helpers;

public static class KiotaHelper
{
    public static async Task<string?> GetValueAsJsonStringAsync(object value)
    {
        if (value is UntypedNode untypedNode)
        {
            return await untypedNode.GetOriginalJsonAsync();
        }

        return JsonUtilities.DefaultSerialize(value);
    }

    public static async Task<string> GetOriginalJsonAsync(this UntypedNode untypedNode)
    {
        return await KiotaJsonSerializer.SerializeAsStringAsync(untypedNode);
    }
}