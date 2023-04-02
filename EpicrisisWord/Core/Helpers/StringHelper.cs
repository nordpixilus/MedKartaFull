using System;
using System.IO;
using System.Windows;

namespace EpicrisisWord.Core.Helpers;

internal static class StringHelper
{
    internal static (string fileName, string pathFile) GetNewNameFile(string ini, string shortMedicftion)
    {


        string newNameFile = $"{ini} Эпикриз {shortMedicftion}.docx";
        string specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string? specialFolderPathFile = Path.Combine(specialFolder, $"{newNameFile}");
        if (File.Exists(specialFolderPathFile))
        {
            string currentYear = DateTime.Now.Year.ToString();
            newNameFile = $"{ini} Эпикриз {shortMedicftion} {currentYear}.docx";
            specialFolderPathFile = Path.Combine(specialFolder, $"{newNameFile}");
        }

        return (newNameFile, specialFolderPathFile);
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

    internal static string ExtractMedication(string medication)
    {
        if (string.IsNullOrEmpty(medication))
        {
            return string.Empty;
        }
        else if(medication.Contains(" шейного "))
        {
            return "ШОП";
        }
        else if (medication.Contains("  грудного "))
        {
            return "ГОП";
        }
        else if (medication.Contains("  пояснично "))
        {
            return "ПОП";
        }
        else
        {
            MessageBox.Show("Не смог разобрать тип заболевания");
            return string.Empty;
        }
    }
}
