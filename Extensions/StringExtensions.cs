using System;

namespace TPDMS.RestApi.Extensions
{
    public static class StringExtensions
    {
        public static bool ToBoolean(this string str)
        {
            var cleanValue = (str ?? "").Trim();
            if (string.Equals(cleanValue, "False", StringComparison.OrdinalIgnoreCase))
                return false;
            return
                (string.Equals(cleanValue, "True", StringComparison.OrdinalIgnoreCase)) ||
                (cleanValue != "0");
        }
    }
}