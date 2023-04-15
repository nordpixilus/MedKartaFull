using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MedKarta.Core.Helpers;

internal static class RegexHelper
{
    internal static Dictionary<string, string> ExtractFieldsPerson(string textProblem)
    {
        Dictionary<string, string> fields = new();


        string pattern = @"  +|(\r+\n+)+";
        Regex regex = new(pattern);
        textProblem = regex.Replace(textProblem, " ");



        Dictionary<string, string> patterns = new()
        {
            { "FullName", @"^(?<FullName>([а-яА-Я]+ ?){2,5}),? (Д|д)ата" },
            { "BirthDateAll", @"^.+рождения(:|,|.|;)?\s?(?<BirthDateAll>.{18,20})(:|,|.|;)?\s(П|п)ол" },
            { "BirthDate", @"^.+рождения(:|,|.|;)?\s?(?<BirthDate>(\d{2}\.){2}\d{4}) \(" },
            { "AgeInt", @"^.+\((?<AgeInt>\d{2}) (лет|года?)\)(,|\.)?\s(П|п)ол" },
            { "AgeStr", @"^.+\(\d{2} (?<AgeStr>(лет|года?))\)(,|\.)?\s(П|п)ол" },
            { "Gender", @"^.+(П|п)ол(:|,|.|;)?\s?(?<Gender>(муж|жен|Муж|Жен|М|Ж))(:|,|.|;)?\s(К|к)од" },
            { "Kod", "" },
            { "Reg", @"^.+Адрес регистрации: ?(?<Reg>.+) Адрес" },
            { "Res", @"^.+Адрес проживания: ?(?<Res>.+)(,?|\.?) Занятость" },
            { "Work", @"^.+Занятость:\s?(?<Work>.*)\s(Теле|Кон)" },
            //{ "date_start", "" },
            //{ "date_end", "" }
        };

        foreach (KeyValuePair<string, string> entry in patterns)
        {
            fields[entry.Key] = ParseText(entry.Key, entry.Value, textProblem);
        }

        fields["Geder"] = ConverGender(fields["Gender"]);
        return fields;
    }

    private static string ConverGender(string gender)
    {
        if(gender.Length == 1)
        {
            return gender;
        }
        else if (gender.Length == 3)
        {
            if (gender.ToLower() == "муж") return "М";
            if (gender.ToLower() == "жен") return "Ж";
        }

        return string.Empty;
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
