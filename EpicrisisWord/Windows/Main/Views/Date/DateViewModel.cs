using CommunityToolkit.Mvvm.ComponentModel;
using EpicrisisWord.Core.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Windows.Documents;
using System.Windows;

namespace EpicrisisWord.Windows.Main.Views.Date;

internal partial class DateViewModel : BaseViewModel
{
    #region Поле DateStart Дата начала

    //public bool IsErrorDate { get; set; } = true;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private DateTime? _DateStart = null;// = (DateTime.Today).Subtract(TimeSpan.FromDays(30));

    //partial void OnDateStartChanged(DateTime? value)
    //{
    //    ValidateAllProperties();
    //    IsErrorDate = HasErrors;

    //    //ButtonCreateDocumentChangedEnabled(HasErrors);
    //}

    #endregion

    #region Поле DateEnd Дата окончания

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private DateTime? _DateEnd = null;

    //partial void OnDateEndChanged(DateTime? value)
    //{
    //    ValidateAllProperties();
    //    IsErrorDate = HasErrors;

    //    //ButtonCreateDocumentChangedEnabled(HasErrors);
    //}

    #endregion

    public DateViewModel()
    {
        ValidateAllProperties();
    }
}
