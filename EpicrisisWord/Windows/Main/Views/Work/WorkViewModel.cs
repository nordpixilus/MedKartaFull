using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using EpicrisisWord.Windows.Main.Views.Date;
using EpicrisisWord.Windows.Main.Views.ListFile;
using EpicrisisWord.Windows.Main.Views.PersonForm;
using System.Windows;

namespace EpicrisisWord.Windows.Main.Views.Work;

internal partial class WorkViewModel : BaseViewModel, IRecipient<PathFileMessage>
{
    [ObservableProperty]
    private DateViewModel _DateContent;

    [ObservableProperty]
    private ListFileViewModel _ListContent;

    [ObservableProperty]
    private PersonFormViewModel _PersonFormContent;

    

   

    public WorkViewModel(string fieldsText)
    {
        WeakReferenceMessenger.Default.Register(this);
        // Важна очерёдность подключений
        DateContent = new DateViewModel();
        ListContent = new ListFileViewModel();
        PersonFormContent = new PersonFormViewModel();
        PersonFormContent.SetFiedsPerson(fieldsText);

    }

    public void Receive(PathFileMessage message)
    {
        MessageBox.Show(DateContent.IsErrorDate.ToString());
        //if (DateContent.IsErrorDate)
        //{
        //    MessageBox.Show("ggg");
        //}
        //else
        //{
        //    MessageBox.Show("111");
        //}
    }
}
