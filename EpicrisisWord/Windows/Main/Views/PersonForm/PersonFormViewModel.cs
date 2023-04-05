using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace EpicrisisWord.Windows.Main.Views.PersonForm;

internal partial class PersonFormViewModel : BaseViewModel
{
    #region Поля Form

    // Поле FullName. Полное имя Ф.И.О
    #region Поле FullName

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [RegularExpression(@"^([А-Я][а-я]+ ?){2,5}", ErrorMessage = "Invalid Social Security Number.")]
    private string _FullName = string.Empty;

    partial void OnFullNameChanged(string value)
    {
        ActionChangeField();
    }

    #endregion

    #region Поле BirthDateFull Дата рождения и возраст    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _BirthDateFull = string.Empty;

    partial void OnBirthDateFullChanged(string value)
    {
        ActionChangeField();
    }

    #endregion

    #region Поле Reg Адрес регистрации    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _Reg = string.Empty;

    partial void OnRegChanged(string value)
    {
        ActionChangeField();
    }

    #endregion

    #region Поле Res Поле Адрес проживания    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]   
    private string _Res = string.Empty;

    partial void OnResChanged(string value)
    {
        ActionChangeField();
    }

    #endregion    

    #endregion

    [ObservableProperty]
    private string _OldWork = string.Empty;

    public PersonFormViewModel()
    {
        ValidateAllProperties();
    }

    private void ActionChangeField()
    {
        Messenger.Send(new ChangeFieldBlockMessege(string.Empty));
    }

    /// <summary>
    /// Выборка из текста нужных значений.
    /// </summary>
    /// <param name="fieldsText"></param>
    public void SetFiedsPerson(string? fieldsText)
    {
        (Dictionary<string, string> boardFields, bool isFields) = RegexHelper.ExtractFieldsPerson(fieldsText);
        if (isFields)
        {
            BirthDateFull = StringHelper.CreateBirtDateFull(boardFields["birth_date"], boardFields["age_int"], boardFields["age_str"]);
            FullName = boardFields["full_name"];
            Reg = boardFields["reg"];
            Res = boardFields["res"];


        }
    }

    public void AddDictionaryFielsPerson(ref Dictionary<string, string> dict)
    {
        dict["birth_date_full"] = BirthDateFull;
        dict["full_name"] = FullName;
        dict["reg"] = Reg;
        dict["res"] = Res;
    }

}
