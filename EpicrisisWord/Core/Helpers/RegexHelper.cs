﻿using System;
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
            { "full_name", @"^(?<full_name>([а-яА-Я]+ ?){2,5}),? (Д|д)ата" },
            { "birth_date", @"^.+рождения(:|,|.|;)?\s?(?<birth_date>(\d{2}\.){2}\d{4}) \(" },
            { "age_int", @"^.+\((?<age_int>\d{2}) (лет|года?)\)(,|\.)?\s(П|п)ол" },
            { "age_str", @"^.+\(\d{2} (?<age_str>(лет|года?))\)(,|\.)?\s(П|п)ол" },
            { "gender", @"^.+(П|п)ол(:|,|.|;)?\s?(?<gender>(муж|жен|Муж|Жен|М|Ж))(:|,|.|;)?\s(К|к)од" },
            //{ "cod", "" },
            { "reg", @"^.+Адрес регистрации: ?(?<reg>.+) Адрес" },
            { "res", @"^.+Адрес проживания: ?(?<res>.+)(,?|\.?) Занятость" },
            //{ "work", @"^.+Занятость: ?(?<work>.*) (Теле|Кон)" },
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

    internal static (Dictionary<string, string> fiedlsPerson, bool isFields) ExtractTextProblem(string text)
    {       
        string pattern = @"  +|\r\n?";
        Regex regex = new(pattern);
        text = regex.Replace(text, " ");
       
        string patternDoctor = @"(Врач.*)";
        Regex regexDoctor = new(patternDoctor);
        text = regexDoctor.Replace(text, "");

        text = text.Trim();
       
        Dictionary<string, string> fiedlsPerson = new();

        Dictionary<string, string> patterns = new()
        {
            { "anamnesis_problem", @"^.*АНАМНЕЗ\s+ЗАБОЛЕВАНИЯ(:|,|.|;)?\s*(?<anamnesis_problem>.*)\sАНАМНЕЗ\s+ЖИЗНИ" },
            { "problem", @"^.*Диагноз основной(:|,|.|;)?\s*(?<problem>.*)\sОсложнения" },
            { "super_problem", @"^.*Осложнения(:|,|.|;)?\s*(?<super_problem>.*)\sСопутствующ(ий|ие\sзаболевания)" },
            { "parallel_problem", @"^.*Сопутствующ(ий|ие\sзаболевания)(:|,|.|;)?\s*(?<parallel_problem>.*)\sПлан\sобследования" },
            { "work", @"^.*СТРАХОВОЙ\sАНАМНЕЗ(:|,|.|;)?\s*(?<work>.*)\sДАННЫЕ" },
            { "medication", @"^.*План\s(лечения|ведения)(:|,|.|;)?\s*(?<medication>.*)$" },
        };

        foreach (KeyValuePair<string, string> entry in patterns)
        {
            fiedlsPerson[entry.Key] = ParseText(entry.Key, entry.Value, text);
        }

        fiedlsPerson["problem2"] = fiedlsPerson["problem"];
        fiedlsPerson["super_problem2"] = fiedlsPerson["super_problem"];
        fiedlsPerson["parallel_problem2"] = fiedlsPerson["parallel_problem"];

        return (fiedlsPerson, true);
    }

    //https://www.cyberforum.ru/csharp-beginners/thread1230635.html  
    internal static void AddExtractIni(ref Dictionary<string, string> fiedlsPerson)
    {
        fiedlsPerson["ini"] = ExtractIni(fiedlsPerson["full_name"]);
    }

    private static string ExtractIni(string full_name)
    {
        string pattern = "(?<F>[а-яА-Я]+)(?:(?:[^а-яА-Я]+)(?<I>[а-яА-Я]+)(?:(?:[^а-яА-Я]+)(?<O>[а-яА-Я]+))?)?";

        Regex regex = new(pattern);

        var match = regex.Match(full_name);

        if (!match.Success)
            return string.Empty; //подсунули дрянь :)

        var inits = match.Groups;
        if (inits["O"].Success)
            return string.Format("{0} {1}. {2}.", inits["F"], inits["I"].Value[0], inits["O"].Value[0]);
        if (inits["I"].Success)
            return string.Format("{0} {1}. ", inits["F"], inits["I"].Value[0]);
        return inits["F"].Value;
    }
}
