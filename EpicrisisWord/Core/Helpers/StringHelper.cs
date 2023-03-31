using System;
using System.IO;

namespace EpicrisisWord.Core.Helpers;

internal static class StringHelper
{
    internal static (string fileName, string pathFile) GetFileName(string ini)
    {


        string fileName = $"{ini} Эпикриз.docx";
        string specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string? pathFile = Path.Combine(specialFolder, $"{fileName}");
        if (File.Exists(pathFile))
        {
            string currentYear = DateTime.Now.Year.ToString();
            fileName = $"{ini} Эпикриз {currentYear}.docx";
            pathFile = Path.Combine(specialFolder, $"{fileName}");
        }

        return (fileName, pathFile);
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
}
