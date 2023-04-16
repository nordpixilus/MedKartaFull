using System.Text.RegularExpressions;

namespace MedKarta.Core.Extensions.StringExtensions;

internal static class SplitCamelCaseExtension
{
    internal static string SplitCamelCase(this string nameClass)
    {
        if (nameClass != null)
        {
            string[] split = Regex.Split(nameClass, @"(?<!^)(?=[A-Z])");
            return split[1];
        }

        return  "";
    }
}
