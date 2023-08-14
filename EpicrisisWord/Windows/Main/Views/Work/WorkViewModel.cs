using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Windows.Main.Views.Date;
using EpicrisisWord.Windows.Main.Views.ListFile;
using EpicrisisWord.Windows.Main.Views.PersonForm;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace EpicrisisWord.Windows.Main.Views.Work;

internal partial class WorkViewModel : BaseViewModel, IRecipient<CreateDocumentMessage>

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
        UpdateListFiles();
        //WeakReferenceMessenger.Default.Register<CreateDocumentMessage>(this);        
        //WeakReferenceMessenger.Default.Register<ChangeFieldsPersonMessege>(this);

    }

    public void Receive(CreateDocumentMessage message)
    {
        if (!PersonFormContent.HasErrors && !DateContent.HasErrors)
        {
            CreateDocumentAsync(message.Value);
        }
        else
        {
            MessageBox.Show("Поля даты не заполнены");
            ListContent.SelectedItemListViewFile = null;
        }
    }

    private async void CreateDocumentAsync(string pathFile)
    {
        string textProblem = TextDocumentHelper.GetTextProblem(pathFile);

        if (!string.IsNullOrEmpty(textProblem))
        {
            (fiedlsPerson, bool isFields) = RegexHelper.ExtractTextProblem(textProblem);
            if (isFields)
            {
                IsCreateFileDirection();
                AddFiedlsPersonValue(pathFile);
                CreateOpenFiles();
                await Task.Delay(3000);
                Application.Current.Windows[0].Close();
            }
        }
    }

    private void IsCreateFileDirection()
    {
        string sMessageBoxText = "Создать документ: Направление?";
        string sCaption = "Создание документа.";
        MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
        MessageBoxImage icnMessageBox = MessageBoxImage.Question;
        MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        switch (rsltMessageBox)
        {
            // Добавляем значение для создания документа направление
            case MessageBoxResult.Yes: fiedlsPerson["check_direction"] = "true"; break;
            case MessageBoxResult.No: fiedlsPerson["check_direction"] = "false"; break;
        }
    }

    private void AddFiedlsPersonValue(string pathFile)
    {
        // Добавляем в список поля пациента.
        PersonFormContent.AddDictionaryFielsPerson(ref fiedlsPerson);
        // Добавляем поля с датой.
        DateContent.AddDictionaryFielsDate(ref fiedlsPerson);
        // Добавляем поле с коротким названием заболевания.
        StringHelper.AddExtractMedication(ref fiedlsPerson);
        // Добавляем поле с рекомендацией по лечению.
        StringHelper.AddExtractRecommendation(ref fiedlsPerson);
        // Добавляем поле с инициалами.
        RegexHelper.AddExtractIni(ref fiedlsPerson);
        // Добавление путей к файлам шаблона
        StringHelper.AddPathTemplateFiles(ref fiedlsPerson);
        // Добавляем путь файлу первичного осмотра
        fiedlsPerson["primary_file"] = pathFile;
        // Добавляем пути к файлам для печати
        StringHelper.AddPathNewFile(ref fiedlsPerson);
        // Добавляем новое название файла и путь к нему.
        StringHelper.AddNewNameEpicrisisFile(ref fiedlsPerson);
    }

    private void CreateOpenFiles()
    {
        var helper = new WordHelper(fiedlsPerson);
        // Открытие первичного осмотра
        DocumentHelper.OpenDocumentToPath(fiedlsPerson["primary_file"]);
        // Создание и открытие файла эпикриз
        helper.CreateWordFile("Epicrisis");
        DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewEpicrisisFile"]);
        // Создание и открытие файла диагноз
        helper.CreateWordFile("Diagnosis");
        DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewDiagnosisFile"]);
        // Создание и открытие файла направления
        if (fiedlsPerson["check_direction"] == "true")
        {
            // Добавляем поле с заболеванием для направления
            StringHelper.AddFieldProblemDirection(ref fiedlsPerson);
            // Добавляем поле с гинекологом
            StringHelper.AddFieldGynecolog(ref fiedlsPerson);

            helper.CreateWordFile("Direction");
            DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewDirectionFile"]);
        }


        if (fiedlsPerson["short_medicftion"] == "Диабет")
        {
            // Добавляем поля с датами приёма
            StringHelper.AddFieldsDiabet(ref fiedlsPerson);
            // Создание и открытие файла с диабетом
            helper.CreateWordFile("Diabet");
            DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewDiabetFile"]);
        }
    }

    /// <summary>
    /// Обработка сообщения при изменении любого поля на форме ввода личных данных.
    /// </summary>
    /// <param fullName="message"></param>
    //public void Receive(ChangeFieldsPersonMessege message)
    //{
    //    UpdateListFiles();
    //}

    /// <summary>
    /// Вызов обновления списка документов
    /// </summary>
    private void UpdateListFiles()
    {
        Messenger.Send(new UpdateListFileMessage(PersonFormContent.FullName));
    }
}
