using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Windows.Main.Views.Date;
using EpicrisisWord.Windows.Main.Views.ListFile;
using EpicrisisWord.Windows.Main.Views.PersonForm;
using System.Collections.Generic;

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

    private Dictionary<string, string> fiedlsPerson = new();


    public WorkViewModel(string fieldsText)
    {
        // Заполнение данными полей в PersonFormViewModel
        PersonFormContent.SetFiedsPerson(fieldsText);
        WeakReferenceMessenger.Default.RegisterAll(this);
        CheckValidationBlock();
        //WeakReferenceMessenger.Default.Register<CreateDocumentMessage>(this);        
        //WeakReferenceMessenger.Default.Register<ChangeFieldBlockMessege>(this);

    }

    public async void Receive(CreateDocumentMessage message)
    {
        string textProblem = TextDocumentHelper.GetTextProblem(message.Value);        
        
        if (!string.IsNullOrEmpty(textProblem))
        {
            (fiedlsPerson, bool isFields) = RegexHelper.ExtractTextProblem(textProblem);
            if (isFields)
            {
                CreateDocuments(message.Value);               
                await System.Threading.Tasks.Task.Delay(3000);
                System.Windows.Application.Current.Windows[0].Close();
            }

            
        }
        //string? textProblem = await ClipoardHelper.StartMoninitorTextProblemAsync();        
    }

    private void CreateDocuments(string pathFile)
    {
        // Добавляем в список поля пациента.
        PersonFormContent.AddDictionaryFielsPerson(ref fiedlsPerson);
        // Добавляем поля с датой.
        DateContent.AddDictionaryFielsDate(ref fiedlsPerson);
        // Добавляем поле с коротким названием заболевания.
        StringHelper.AddExtractMedication(ref fiedlsPerson);
        // Добавляем поле с рекомендацией.
        StringHelper.AddExtractRecommendation(ref fiedlsPerson);
        // Добавляем поле с инициалами.
        RegexHelper.AddExtractIni(ref fiedlsPerson);
        // Добавление путей к файлам
        StringHelper.AddPathTemplateFiles(ref fiedlsPerson);
        // Добавляем путь к файлу с файлу диагноза для печати
        StringHelper.AddPathNewDiagnosisFile(ref fiedlsPerson);
        // Добавляем новое название файла и путь к нему.
        StringHelper.AddNewNameEpicrisisFile(ref fiedlsPerson);


        var helper = new WordHelper(fiedlsPerson);
        helper.CreateEpicrisisFile();
        helper.CreateDiagnosisFile();

        DocumentHelper.OpenDocumentToPath(pathFile);
        DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewEpicrisisFile"]);
        DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewDiagnosisFile"]);
    }    

    /// <summary>
    /// Обработка сообщения при изменении любого поля на форме ввода личных данных.
    /// </summary>
    /// <param name="message"></param>
    public void Receive(ChangeFieldBlockMessege message)
    {
        CheckValidationBlock();
    }

    /// <summary>
    /// Проверка ошибок по всем полям формы личных данных пациента. 
    /// </summary>
    // При отсутствии ошибок вызывается обновление списка документов и
    // возможность дейстия выбора в списке.
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
