using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace EpicrisisWord.Core.Helpers;

internal static class StringHelper
{
    internal static void AddNewNameFile(ref Dictionary<string, string> boardFields)
    {
        string ini = boardFields["ini"];
        string short_medicftion = boardFields["short_medicftion"];

        string newNameFile = $"{ini} Эпикриз {short_medicftion}.docx";
        string specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string? specialFolderPathFile = Path.Combine(specialFolder, $"{newNameFile}");
        if (File.Exists(specialFolderPathFile))
        {
            string currentYear = DateTime.Now.Year.ToString();
            newNameFile = $"{ini} Эпикриз {short_medicftion} {currentYear}.docx";
            specialFolderPathFile = Path.Combine(specialFolder, $"{newNameFile}");
        }

        boardFields["newNameFile"] = newNameFile;
        boardFields["specialFolderPathFile"] = specialFolderPathFile;
    }

    /// <summary>
    /// Собирает полную строку с датой рождения и возрастом.
    /// </summary>
    /// <param name="birth_date"></param>
    /// <param name="age_int"></param>
    /// <param name="age_str"></param>
    /// <returns></returns>
    internal static string CreateBirtDateFull(string birth_date, string age_int, string age_str)
    {
        return string.Concat(birth_date, " (", age_int, " ", age_str, ")");
    }

    /// <summary>
    /// Заменяет значения для поля занятость.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    internal static string SetDefautValueWork(string value)
    {
        return value switch
        {
            "пенсионер" => "Не работает, пенсионер",
            "Пенсионер" => "Не работает, пенсионер",
            "п/с" => "Не работает, пенсионер",
            "П/С" => "Не работает, пенсионер",
            "п/С" => "Не работает, пенсионер",
            "П/с" => "Не работает, пенсионер",
            "н/р" => "Не работает",
            "Н/Р" => "Не работает",
            "р/Р" => "Не работает",
            "Н/р" => "Не работает",
            "не указано" => string.Empty,
            "Не указано" => string.Empty,
            _ => value,
        };
    }

    internal static void AddExtractMedication(ref Dictionary<string, string> boardFields)
    {
        string problem = boardFields["problem"];

        if (problem.Contains(" шейного "))
        {
            boardFields["short_medicftion"] = "ШОП";
        }
        else if (problem.Contains("  грудного "))
        {
            boardFields["short_medicftion"] = "ГОП";
        }
        else if (problem.Contains("  пояснично"))
        {
            boardFields["short_medicftion"] = "ПОП";
        }
        else
        {
            boardFields["short_medicftion"] = string.Empty;
        }
    }

    internal static void AddExtractRecommendation(ref Dictionary<string, string> boardFields)
    {
        boardFields["recommendation"] = boardFields["short_medicftion"] switch
        {
            "ШОП" => Properties.Settings.Default.recom_hop,
            "ПОП" => Properties.Settings.Default.recom_pop,
            "ГОП" => Properties.Settings.Default.recom_gop,
            _ => string.Empty,
        };
    }
}
