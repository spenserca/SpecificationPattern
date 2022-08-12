using Microsoft.Extensions.Primitives;

namespace SpecificationPattern.Functions;

public static class QueryStringExtensions
{
    public static string GetQueryStringValue(this IReadOnlyDictionary<string, StringValues> querystring, string key)
    {
        if (querystring.ContainsKey(key))
        {
            return querystring[key];
        }

        return string.Empty;
    }
}