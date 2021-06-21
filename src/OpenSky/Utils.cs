using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace OpenSky
{
    public static class Utils
    {
        public static string CreateQuery(Dictionary<string, object> dict)
        {
            if (dict != null && dict.Count > 0)
            {
                return "?" + string.Join("&", dict.Keys.Select(x =>
                {
                    if (dict[x] is IEnumerable<object> e)
                    {
                        return string.Join("&", e.Select(v => $"{x}={v}"));
                    }
                    return $"{x}={dict[x]}";
                }));
            }
            return string.Empty;
        }
    }
}