using CommunityToolkit.Mvvm.ComponentModel;
using EpicrisisWord.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EpicrisisWord.Windows.Main.Views.Date;

internal partial class DateViewModel : BaseViewModel
{
    #region Поле DateStart Дата начала

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private DateTime? _DateStart = null;// = (DateTime.Today).Subtract(TimeSpan.FromDays(30));

    #endregion

    #region Поле DateEnd Дата окончания

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private DateTime? _DateEnd = null;

    #endregion

    public DateViewModel()
    {
        ValidateAllProperties();
    }

    public void AddDictionaryFielsDate(ref Dictionary<string, string> dict)
    {
        dict["date_start"] = DateStart!.Value.ToShortDateString();
        dict["date_end"] = DateEnd!.Value.ToShortDateString();
        dict["date_end2"] = DateEnd!.Value.ToShortDateString();
        dict["date_direction"] = DateStart!.Value.Subtract(TimeSpan.FromDays(1)).ToShortDateString();
    }
}
