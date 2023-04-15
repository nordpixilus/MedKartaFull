using MedKarta.Core.Helpers;
using MedKarta.UCL.ErrorBoard.ErrorKod;
using System.Collections.Generic;

namespace MedKarta.Core.Models;

class PersonWork
{
    internal int? Id { get; set; } = null;

    internal string? Kod { get; set; } = null;

    public string? FullName { get; set; } = null!;

    public string? Gender { get; set; } = null!;

    public string? BirthDateAll { get; set; } = null;

    public string? BirthDate { get; set; } = null!;

    public string? AgeInt { get; set; } = null!;

    public string? AgeStr { get; set; } = null!;

    public string? Work { get; set; } = null!;

    public string? Reg { get; set; } = null!;

    public string? Res { get; set; } = null!;

    public void SetFields(string fieldsText)
    {
        // Выборка из текста нужных значений.
        Dictionary<string, string> boardFields = RegexHelper.ExtractFieldsPerson(fieldsText!);
        Kod = boardFields["Kod"];
        FullName = boardFields["FullName"];
        Gender = boardFields["Gender"];
        BirthDateAll = boardFields["BirthDateAll"];
        BirthDate = boardFields["BirthDate"];
        AgeInt = boardFields["AgeInt"];
        AgeStr = boardFields["AgeStr"];
        Work = boardFields["Work"];
        Reg = boardFields["Reg"];
        Res = boardFields["Res"];

        //if (isFields)
        //{
        //    FullName = boardFields["full_name"];
        //    BirthDateFull = StringHelper.CreateBirtDateFull(boardFields["birth_date"], boardFields["age_int"], boardFields["age_str"]);
        //    Reg = boardFields["reg"];
        //    Res = boardFields["res"];
        //    _gender = boardFields["gender"];
        //}
    }

    public string MyEquals(object? obj)
    {
        return "o";
    }

    internal string? IsErrorFields()
    {
       if(string.IsNullOrEmpty(Kod)) return nameof(ErrorKodViewModel);

        return string.Empty;
    }
}
