﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    private string _gender { get; set; } = string.Empty;

    #endregion

    public PersonFormViewModel()
    {
        ValidateAllProperties();
    }

    /// <summary>
    /// Вызывается при изменении любого поля формы пачиента.
    /// Посылает сообжение об изменении.
    /// </summary>
    private void ActionChangeField()
    {
        Messenger.Send(new ChangeFieldsPersonMessege(string.Empty));
    }

    /// <summary>
    /// Заполнение полей формы.
    /// </summary>
    /// <param name="fieldsText"></param>
    public void SetFiedsPerson(string? fieldsText)
    {
        // Выборка из текста нужных значений.
        (Dictionary<string, string> boardFields, bool isFields) = RegexHelper.ExtractFieldsPerson(fieldsText!);
        if (isFields)
        {            
            FullName = boardFields["full_name"];
            BirthDateFull = StringHelper.CreateBirtDateFull(boardFields["birth_date"], boardFields["age_int"], boardFields["age_str"]);
            Reg = boardFields["reg"];
            Res = boardFields["res"];
            _gender = boardFields["gender"];
        }
    }

    /// <summary>
    /// Добавляет в список значения полей: full_name, direction, birth_date_full, reg, res
    /// </summary>
    /// <param name="dict"></param>
    public void AddDictionaryFielsPerson(ref Dictionary<string, string> dict)
    {        
        dict["full_name"] = FullName;        
        dict["birth_date_full"] = BirthDateFull;
        dict["reg"] = Reg;
        dict["res"] = Res;
        dict["gender"] = _gender;
    }

}
