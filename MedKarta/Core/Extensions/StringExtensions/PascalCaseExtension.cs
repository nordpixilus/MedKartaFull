using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

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

    public static string? SplitPascalCase(this string? nameClass)
    {
        if (!nameClass.IsPascalCase()) return null;

        return Regex.Split(nameClass!, @"(?<!^)(?=[A-Z])")[0];
    }

    [GeneratedRegex("^[a-zA-Z]+$")]
    private static partial Regex MyRegex();
}
