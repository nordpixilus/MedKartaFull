using ICSharpCode.SharpZipLib.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace EpicrisisWord.Core.Helpers;

internal static class StringHelper
{
    internal static void AddNewNameEpicrisisFile(ref Dictionary<string, string> fiedlsPerson)
    {
        string ini = fiedlsPerson["ini"];
        string short_medicftion = fiedlsPerson["short_medicftion"];
        string specialFolder = fiedlsPerson["specialFolder"];

        string newNameEpicrisisFile = $"{ini} Эпикриз {short_medicftion}.docx";
        
        string? pathNewEpicrisisFile = Path.Combine(specialFolder, $"{newNameEpicrisisFile}");
        if (File.Exists(pathNewEpicrisisFile))
        {
            string currentYear = DateTime.Now.Year.ToString();
            newNameEpicrisisFile = $"{ini} Эпикриз {short_medicftion} {currentYear}.docx";
            pathNewEpicrisisFile = Path.Combine(specialFolder, $"{newNameEpicrisisFile}");
        }

        fiedlsPerson["newNameEpicrisisFile"] = newNameEpicrisisFile;
        fiedlsPerson["pathNewEpicrisisFile"] = pathNewEpicrisisFile;
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

    internal static void AddExtractMedication(ref Dictionary<string, string> fiedlsPerson)
    {
        string problem = fiedlsPerson["problem"].ToLower();
        fiedlsPerson["short_medicftion"] = string.Empty;

        Dictionary<string, string> keyValuePairs = new()
        {
            { "ШОП", " шейного " },
            { "ГОП", " грудного " },
            { "ПОП", " пояснично" },
            { "Диабет", "сахарный " },
            { "ДЭ", "энцефалопатия " },
            { "Атеро", "атеросклероз" }
        };

        foreach (var (key, value) in keyValuePairs)
        {
            if (problem.Contains(value))
            {
                fiedlsPerson["short_medicftion"] = key;
                break;
            }
        }        
    }

    internal static void AddExtractRecommendation(ref Dictionary<string, string> fiedlsPerson)
    {
        fiedlsPerson["recommendation"] = fiedlsPerson["short_medicftion"] switch
        {
            "ШОП" => Properties.Settings.Default.recom_hop,
            "ПОП" => Properties.Settings.Default.recom_pop,
            "ГОП" => Properties.Settings.Default.recom_gop,
            "Диабет" => Properties.Settings.Default.recom_diabet,
            "ДЭ" => Properties.Settings.Default.recom_de,
            "Атеро" => Properties.Settings.Default.recom_atero,
            _ => string.Empty,
        };
    }

    internal static void AddPathTemplateFiles(ref Dictionary<string, string> fiedlsPerson)
    {
        fiedlsPerson["specialFolder"] = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string pathTemp = "Temp";

        DirectoryInfo dirInfoTemp = new(pathTemp);
        if (!dirInfoTemp.Exists) dirInfoTemp.Create();

        FileInfo fileInfoEpicrisis =  new(Path.Combine("TemplatesWord", "epicrisis.docx"));
        fiedlsPerson["pathEpicrisisFile"] = fileInfoEpicrisis.FullName;

        FileInfo fileInfoDiagnosis = new(Path.Combine("TemplatesWord", "diagnosis.docx"));
        fiedlsPerson["pathDiagnosisFile"] = fileInfoDiagnosis.FullName;

        FileInfo fileInfoDirection = new(Path.Combine("TemplatesWord", "direction.docx"));
        fiedlsPerson["pathDirectionFile"] = fileInfoDirection.FullName;
    }

    internal static void AddPathNewFile(ref Dictionary<string, string> fiedlsPerson)
    {
        FileInfo fileInfoNewDiagnosis = new(Path.Combine("Temp", "NewDiagnosis.docx"));
        fiedlsPerson["pathNewDiagnosisFile"] = fileInfoNewDiagnosis.FullName;

        FileInfo fileInfoNewDirection = new(Path.Combine("Temp", "NewDirection.docx"));
        fiedlsPerson["pathNewDirectionFile"] = fileInfoNewDirection.FullName;
    }

    internal static void AddFieldProblemDirection(ref Dictionary<string, string> fiedlsPerson)
    {
        string spine = " отдела позвоночника";
        string rump = "пояснично — крестцового отдела";
        string fri = " + ФРИ";

        fiedlsPerson["rengen"] = fiedlsPerson["short_medicftion"] switch
        {
            "ШОП" => CreateRengen(dop_section: "шейного", section: spine, countUnderscope: 39, fri: fri),
            "ПОП" => CreateRengen(dop_section: "", section: rump, countUnderscope: 1, fri: fri),
            "ГОП" => CreateRengen(dop_section: "грудного", section: spine, countUnderscope: 39, fri: fri),
            _ => string.Empty,
        };
    }

    private static string CreateRengen(string dop_section, string section, int countUnderscope, string fri)
    {
        StringBuilder rengen = new();
        int lenStr = 87;
        string rg = "Ренгенография ";
        string ot = " от ";

        rengen.Append(rg);
        rengen.Append(dop_section);
        rengen.Append(section);
        rengen.Append(fri);
        rengen.Append(ot);
        rengen.Append(AddUnderscore(countUnderscope));
        rengen.Append(AddUnderscore(lenStr));
        return rengen.ToString();
    }    

    internal static void AddFieldGynecolog(ref Dictionary<string, string> fiedlsPerson)
    {
        if(fiedlsPerson["gender"][..1].ToLower() == "ж")
        {
            fiedlsPerson["gynecolog"] = CreateGynecolog();
        }
        else
        {
            fiedlsPerson["gynecolog"] = string.Empty;
        }
    }

    private static string CreateGynecolog()
    {
        StringBuilder gynecolog = new();

        gynecolog.Append("Осмотр гинеколога (смотровой кабинет)  от ");
        gynecolog.Append(AddUnderscore(49));
        gynecolog.Append(AddUnderscore(87));

        return gynecolog.ToString();
    }

    private static string AddUnderscore(int x)
    {
        StringBuilder line = new("", x);

        for (int i = 1; i < x; i++)
        {
            line.Append("_");
        }
        line.Append(Environment.NewLine);

        return line.ToString();
    }
}
