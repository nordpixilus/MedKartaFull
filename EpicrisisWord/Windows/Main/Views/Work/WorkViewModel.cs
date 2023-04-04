using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using EpicrisisWord.Windows.Main.Views.Date;
using EpicrisisWord.Windows.Main.Views.ListFile;
using EpicrisisWord.Windows.Main.Views.PersonForm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace EpicrisisWord.Windows.Main.Views.Work;

internal partial class WorkViewModel : BaseViewModel, IRecipient<CreateDocumentMessage>, IRecipient<ChangeFieldBlockMessege>
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
        //WeakReferenceMessenger.Default.Register<CreateDocumentMessage>(this);        
        //WeakReferenceMessenger.Default.Register<ChangeFieldBlockMessege>(this);

    }

    public async void Receive(CreateDocumentMessage message)
    {
        DocumentHelper.OpenDocumentToPath(message.Value);
        string? textProblem = await ClipoardHelper.StartMoninitorTextProblemAsync();
        if (textProblem != null)
        {
            // Получаем медицинские поля 
            (Dictionary<string, string> boardFields, bool isFields) = RegexHelper.ExtractTextProblem(textProblem);
            if(isFields)
            {
                // Добавляем в список поля пациента.
                PersonFormContent.AddDictionaryFielsPerson(ref boardFields);
                // Добавляем поля с датой.
                DateContent.AddDictionaryFielsDate(ref boardFields);
                // Добавляем поле с инициалами.
                RegexHelper.AddExtractIni(ref boardFields);
                // Добавляем поле с коротким названием заболевания.
                StringHelper.AddExtractMedication(ref boardFields);
                // Добавляем поле с рекомендацией.
                StringHelper.AddExtractRecommendation(ref boardFields);
                // Добавляем новое название файла и путь к нему.
                StringHelper.AddNewNameFile(ref boardFields);

              
                var helper = new WordHelper(boardFields);
                helper.CreateEpicrisisFile();
                //helper.CreateDiagnosisFile();
                DocumentHelper.OpenDocumentToPath(boardFields["specialFolderPathFile"]);
                await Task.Delay(3000);
                Application.Current.Windows[0].Close();
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
