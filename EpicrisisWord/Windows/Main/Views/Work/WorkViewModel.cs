using CommunityToolkit.Mvvm.ComponentModel;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Windows.Main.Views.Date;
using EpicrisisWord.Windows.Main.Views.ListFile;
using EpicrisisWord.Windows.Main.Views.PersonForm;

namespace EpicrisisWord.Windows.Main.Views.Work;

internal partial class WorkViewModel : BaseViewModel
{
    [ObservableProperty]
    private BaseViewModel? _PersonFormContent;

    [ObservableProperty]
    private BaseViewModel? _DateContent;

    [ObservableProperty]
    private BaseViewModel? _ListContent;

    public WorkViewModel(string fieldsText)
    {
        PersonFormContent = new PersonFormViewModel(fieldsText);
        DateContent = new DateViewModel();
        ListContent = new ListFileViewModel();
    }
}
