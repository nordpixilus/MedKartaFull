using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using EpicrisisWord.Windows.Main.Views.Date;
using EpicrisisWord.Windows.Main.Views.ListFile;
using EpicrisisWord.Windows.Main.Views.PersonForm;
using System.Collections.Generic;
using System.Windows;

namespace EpicrisisWord.Windows.Main.Views.Work;

internal partial class WorkViewModel : BaseViewModel, IRecipient<OpenDocumentMessage>, IRecipient<ChangeFieldBlockMessege>
{
    // Важна очерёдность подключений
    [ObservableProperty]
    private DateViewModel _DateContent = new();

    [ObservableProperty]
    private ListFileViewModel _ListContent = new();

    [ObservableProperty]
    private PersonFormViewModel _PersonFormContent = new();


    public WorkViewModel(string fieldsText)
    {
        //WeakReferenceMessenger.Default.Register(this);

        PersonFormContent.SetFiedsPerson(fieldsText);
        WeakReferenceMessenger.Default.RegisterAll(this);
        CheckValidationBlock();
        //WeakReferenceMessenger.Default.Register<OpenDocumentMessage>(this);        
        //WeakReferenceMessenger.Default.Register<ChangeFieldBlockMessege>(this);

    }

    public async void Receive(OpenDocumentMessage message)
    {
        DocumentHelper.OpenDocumentToPath(message.Value);
        string? textProblem = await ClipoardHelper.StartMoninitorTextProblemAsync();
        if (textProblem != null)
        {
            (Dictionary<string, string> boardFields, bool isFields) = RegexHelper.ExctraxtTextProblem(textProblem);
            if(isFields)
            {
                PersonFormContent.AddDictionaryFielsPerson(ref boardFields);
                DateContent.AddDictionaryFielsDate(ref boardFields);
                var helper = new WordHelper(boardFields);
                helper.ProcessAdd();

            }
        }
        
    }

    public void Receive(ChangeFieldBlockMessege message)
    {
        CheckValidationBlock();
        //MessageBox.Show(PersonFormContent.HasErrors.ToString());
    }

    private void CheckValidationBlock()
    {
        if (PersonFormContent.HasErrors == false)
        {
            Messenger.Send(new UpdateListFileMessage(PersonFormContent.FullName));
        }

        if (PersonFormContent.HasErrors == false && DateContent.HasErrors == false)
        {
            ListContent.IsEnabled = true;
        }
        else
        {
            ListContent.IsEnabled = false;
        }
    }
}
