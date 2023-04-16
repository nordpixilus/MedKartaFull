using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MedKarta.Core.Extensions.StringExtensions;

public static partial class PascalCaseExtension
{
    public static bool IsPascalCase(this string? strPascalCase)
    {
        if (string.IsNullOrWhiteSpace(strPascalCase)) return false;

        if (!MyRegex().IsMatch(strPascalCase)) return false;

        if (!Char.IsUpper(strPascalCase, 0)) return false;

        if(strPascalCase.Count(m => char.IsUpper(m)) == 1) return false;    

        return true;
    }

    public static string SplitPascalCase(this string? nameClass)
    {
        if (nameClass.IsPascalCase())
        {
            ArgumentException.ThrowIfNullOrEmpty("Строка не соответсвует названию класса", nameof(nameClass));
        }

        string[] split = Regex.Split(nameClass!, @"(?<!^)(?=[A-Z])");
        return split[1];
    }

    [GeneratedRegex("^[a-zA-Z]+$")]
    private static partial Regex MyRegex();
}
