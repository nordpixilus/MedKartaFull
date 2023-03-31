using CommunityToolkit.Mvvm.ComponentModel;
using EpicrisisWord.Core.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace EpicrisisWord.Windows.Main.Views.Date;

internal partial class DateViewModel : BaseViewModel
{
    #region Поле DateStart Дата начала    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private DateTime? _DateStart = null;// = (DateTime.Today).Subtract(TimeSpan.FromDays(30));

    partial void OnDateStartChanged(DateTime? value)
    {
        ValidateAllProperties();
        //ButtonCreateDocumentChangedEnabled(HasErrors);
    }

    #endregion

    #region Поле DateEnd Дата окончания

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private DateTime? _DateEnd = null;

    partial void OnDateEndChanged(DateTime? value)
    {
        ValidateAllProperties();
        //ButtonCreateDocumentChangedEnabled(HasErrors);
    }

    #endregion
}
