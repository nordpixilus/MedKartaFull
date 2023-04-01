using CommunityToolkit.Mvvm.ComponentModel;
using EpicrisisWord.Core.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Windows.Documents;
using System.Windows;
using EpicrisisWord.Core.Messages;
using CommunityToolkit.Mvvm.Messaging;

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
        Messenger.Send(new ChangeFieldBlockMessege(string.Empty));
    }
}
