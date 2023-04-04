using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace EpicrisisWord.Core.Helpers;

internal static class RegexHelper
{
    internal static (Dictionary<string, string>, bool) ExtractFieldsPerson(string textProblem)
    {
        Dictionary<string, string> fields = new();
        

        string pattern = @"  +|(\r+\n+)+";
        Regex regex = new(pattern);
        textProblem = regex.Replace(textProblem, " ");

        

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
            fields[entry.Key] = ParseText(entry.Key, entry.Value, textProblem);
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
        string pattern = $"^{name}.*";


        if (Regex.IsMatch(filename, pattern))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal static (Dictionary<string, string> boardFields, bool isFields) ExtractTextProblem(string text)
    {       
        string pattern = @"  +|\r\n?";
        Regex regex = new(pattern);
        text = regex.Replace(text, " ");
       
        string patternDoctor = @"(Врач.*)";
        Regex regexDoctor = new(patternDoctor);
        text = regexDoctor.Replace(text, "");

        text = text.Trim();
        MessageBox.Show(text);
        Dictionary<string, string> fields = new();

        Dictionary<string, string> patterns = new()
        {
            { "problem", @"^.*Диагноз основной(:|,|.|;)\s*(?<problem>.*) Осложнения" },
            { "super_problem", @"^.*Осложнения(:|,|.|;)\s*(?<super_problem>.*) Сопутствующ(ий|ие заболевания)" },
            { "parallel_problem", @"^.*Сопутствующ(ий|ие заболевания)(:|,|.|;)\s*(?<parallel_problem>.*) План обследования" },
            { "medication", @"^.*План (лечения|ведения)(:|,|.|;)\s*(?<medication>.*)$" },
        };

        foreach (KeyValuePair<string, string> entry in patterns)
        {
            fields[entry.Key] = ParseText(entry.Key, entry.Value, text);
        }

        fields["problem2"] = fields["problem"];
        fields["super_problem2"] = fields["super_problem"];
        fields["parallel_problem2"] = fields["parallel_problem"];

        return (fields, true);
    }

    //https://www.cyberforum.ru/csharp-beginners/thread1230635.html    
    internal static void AddExtractIni(ref Dictionary<string, string> boardFields)
    {
        string pattern = "(?<F>[а-яА-Я]+)(?:(?:[^а-яА-Я]+)(?<I>[а-яА-Я]+)(?:(?:[^а-яА-Я]+)(?<O>[а-яА-Я]+))?)?";

        Regex regex = new(pattern);

        var match = regex.Match(boardFields["full_name"]);

        if (!match.Success)
            boardFields["ini"] = string.Empty; //подсунули дрянь :)

        var inits = match.Groups;

        if (inits["O"].Success)
            boardFields["ini"] = string.Format("{0} {1}. {2}.", inits["F"], inits["I"].Value[0], inits["O"].Value[0]);
        
        if (inits["I"].Success)
            boardFields["ini"] = string.Format("{0} {1}. ", inits["F"], inits["I"].Value[0]);

        boardFields["ini"] = inits["F"].Value;
    }
}
