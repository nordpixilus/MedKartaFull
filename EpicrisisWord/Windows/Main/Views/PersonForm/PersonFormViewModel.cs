using CommunityToolkit.Mvvm.ComponentModel;
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
        SetError(HasErrors);
        //ButtonCreateDocumentChangedEnabled(HasErrors);
        //UpdateProperty();
    }

    #endregion

    #region Поле BirthDateFull Дата рождения и возраст    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _BirthDateFull = string.Empty;

    partial void OnBirthDateFullChanged(string value)
    {
        SetError(HasErrors);
    }

    #endregion

    #region Поле Reg Адрес регистрации    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _Reg = string.Empty;

    partial void OnRegChanged(string value)
    {
        SetError(HasErrors);
    }

    #endregion

    #region Поле Res Поле Адрес проживания    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _Res = string.Empty;

    partial void OnResChanged(string value)
    {
        SetError(HasErrors);
    }

    #endregion

    #region Поле Work Занятость    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Name is Required")]
    [MinLength(3, ErrorMessage = "Name Should be at least 3 character")]
    [MaxLength(100)]
    private string _Work = string.Empty;
    partial void OnWorkChanged(string value)
    {
        SetError(HasErrors);
    }

    #endregion

    #endregion

    [ObservableProperty]
    private bool _IsError = true;

    [ObservableProperty]
    private string _OldWork = string.Empty;

    public PersonFormViewModel()
    {
        //SetFiedsPerson(fieldsText);
    }

    private void SetError(bool hasErrors)
    {
        // Проверяет все свойства в текущем экземпляре и обновляет все отслеживаемые ошибки
        ValidateAllProperties();
        if (!HasErrors) { Messenger.Send(new ListFileMessage(FullName)); }
        //if (!HasErrors) { Messenger.Send }
        //IsErrorDate = hasErrors;
    }

    //public void SetFiedsPerson(string? fieldsText)
    //{
    //    ExctraxtFieldsPerson(fieldsText);

    //}

    /// <summary>
    /// Выборка из текста нужных значений.
    /// </summary>
    /// <param name="fieldsText"></param>
    public void SetFiedsPerson(string? fieldsText)
    {
        (Dictionary<string, string> boardFields, bool isFields) = RegexHelper.ExctraxtFieldsPerson(fieldsText);
        if (isFields)
        {
            BirthDateFull = StringHelper.CreateBirtDateFull(boardFields["birth_date"], boardFields["age_int"], boardFields["age_str"]);
            FullName = boardFields["full_name"];
            Reg = boardFields["reg"];
            Res = boardFields["res"];
            OldWork = boardFields["work"];
            Work = StringHelper.SetDefautValueWork(OldWork);


        }
    }

}
