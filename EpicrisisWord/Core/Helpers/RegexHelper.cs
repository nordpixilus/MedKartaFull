using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EpicrisisWord.Core.Helpers;

internal static class RegexHelper
{
    internal static (Dictionary<string, string>, bool) ExctraxtFieldsPerson(string text)
    {
        string pattern = @"  +|(\r+\n+)+";
        Regex regex = new(pattern);
        text = regex.Replace(text, " ");

        Dictionary<string, string> fields = new();

        Dictionary<string, string> patterns = new()
        {
            { "full_name", @"^(?<full_name>([а-яА-Я]+ ?){2,5}),? дата" },
            { "birth_date", @"^.+рождения: ?(?<birth_date>(\d{2}\.){2}\d{4}) \(" },
            { "age_int", @"^.+\((?<age_int>\d{2}) (лет|года?)\)(,|\.)?\sпол" },
            { "age_str", @"^.+\(\d{2} (?<age_str>(лет|года?))\)(,|\.)?\sпол" },
            //{ "gender", "" },
            //{ "cod", "" },
            { "reg", @"^.+Адрес регистрации: ?(?<reg>.+) Адрес" },
            { "res", @"^.+Адрес проживания: ?(?<res>.+)(,?|\.?) Занятость" },
            { "work", @"^.+Занятость: ?(?<work>.*) (Теле|Кон)" },
            //{ "date_start", "" },
            //{ "date_end", "" }
        };

        foreach (KeyValuePair<string, string> entry in patterns)
        {
            fields[entry.Key] = ParseText(entry.Key, entry.Value, text);
        }

        return (fields, true);
    }

    private static string ParseText(string key, string pattern, string text)
    {
        Regex regex = new(pattern, RegexOptions.Singleline);

        var match = regex.Match(text);
        if (!match.Success)
        {
            return string.Empty;
        }

        var inits = match.Groups;
        if (inits[key].Success)
        {
            return inits[key].Value;
        }
        else
        {
            return string.Empty;
        }

    }

    /// <summary>
    /// Проверка совпадения в названии файла с фамилией.
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    internal static bool IsFamilyToPathFile(string filename, string name)
    {
        string pattern = $"^.*({name}).*";


        if (Regex.IsMatch(filename, pattern))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal static (Dictionary<string, string> boardFields, bool isFields) ExctraxtTextProblem(string text)
    {
        string pattern = @"  +|(\r+\n+)+";
        Regex regex = new(pattern);
        text = regex.Replace(text, " ");

        Dictionary<string, string> fields = new();

        Dictionary<string, string> patterns = new()
        {
            { "problem", @"^.*Диагноз основной(:|,|.) ?(?<problem>.*) Осложнения:" },
            { "super_problem", @"^.*Осложнения(:|,|.) ?(?<super_problem>.*) Сопутствующие заболевания:" },
            { "parallel_problem", @"^.*Сопутствующие заболевания(:|,|.) ?(?<parallel_problem>.*) План обследования:" },
            { "medication", @"^.*План лечения(:|,|.) ?(?<medication>.*)( Врач| ?$)" },
        };

        foreach (KeyValuePair<string, string> entry in patterns)
        {
            fields[entry.Key] = ParseText(entry.Key, entry.Value, text);
        }

        return (fields, true);
    }
}
