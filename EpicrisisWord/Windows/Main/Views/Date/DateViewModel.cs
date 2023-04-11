using CommunityToolkit.Mvvm.ComponentModel;
using EpicrisisWord.Core.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Windows.Documents;
using System.Windows;
using EpicrisisWord.Core.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;

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
        ActionChangeField();
    }

    #endregion

    #region Поле DateEnd Дата окончания

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private DateTime? _DateEnd = null;

    partial void OnDateEndChanged(DateTime? value)
    {
        ActionChangeField();
    }

    #endregion

    public DateViewModel()
    {
        ValidateAllProperties();
    }

    private void ActionChangeField()
    {
        Messenger.Send(new ChangeFieldsPersonMessege(string.Empty));
    }

    public void AddDictionaryFielsDate(ref Dictionary<string, string> dict)
    {
        dict["date_start"] = DateStart!.Value.ToShortDateString();
        dict["date_end"] = DateEnd!.Value.ToShortDateString();
        dict["date_end2"] = DateEnd!.Value.ToShortDateString();
        dict["date_direction"] = DateStart!.Value.Subtract(TimeSpan.FromDays(1)).ToShortDateString();
    }
}
