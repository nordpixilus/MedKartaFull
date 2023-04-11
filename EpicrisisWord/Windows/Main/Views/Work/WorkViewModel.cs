using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Windows.Main.Views.Date;
using EpicrisisWord.Windows.Main.Views.ListFile;
using EpicrisisWord.Windows.Main.Views.PersonForm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace EpicrisisWord.Windows.Main.Views.Work;

internal partial class WorkViewModel : BaseViewModel, IRecipient<CreateDocumentMessage>, IRecipient<ChangeFieldsPersonMessege>
{
    // Важна очерёдность подключений
    [ObservableProperty]
    private DateViewModel _DateContent = new();

    [ObservableProperty]
    private ListFileViewModel _ListContent = new();

    [ObservableProperty]
    private PersonFormViewModel _PersonFormContent = new();

    #region Поле выбора создания документа направление

    [ObservableProperty]
    private bool _IsCheckedDirection = false;

    #endregion

    #region Поле выбора применения фильтра

    [ObservableProperty]
    private bool _IsCheckedFilters = true;
    partial void OnIsCheckedFiltersChanged(bool value)
    {
        UpdateListFiles();
    }

    #endregion

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
            MessageBox.Show("Поля не заполнены");
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
                AddFiedlsPersonValue(pathFile);
                CreateOpenFiles();
                await Task.Delay(3000);
                Application.Current.Windows[0].Close();
            }
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
        // добавляем поле с заболеванием для направления
        StringHelper.AddFieldProblemDirection(ref fiedlsPerson);
        // Добавляем поле с рекомендацией.
        StringHelper.AddExtractRecommendation(ref fiedlsPerson);
        // Добавляем поле с инициалами.
        RegexHelper.AddExtractIni(ref fiedlsPerson);
        // Добавление путей к файлам
        StringHelper.AddPathTemplateFiles(ref fiedlsPerson);
        // Добавляем значение создания документа направление
        fiedlsPerson["check_direction"] = IsCheckedDirection ? "true" : "false";
        // Добавляем путь первичному файлу
        fiedlsPerson["primary_file"] = pathFile;
        // Добавляем путь к файлу с файлу диагноза для печати
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
        helper.CreateEpicrisisFile();
        DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewEpicrisisFile"]);
        // Создание и открытие файла диагноз
        helper.CreateDiagnosisFile();
        DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewDiagnosisFile"]);
        // Создание и открытие файла направления
        if (fiedlsPerson["check_direction"] == "true")
        {
            helper.CreateDirectionFile();
            DocumentHelper.OpenDocumentToPath(fiedlsPerson["pathNewDirectionFile"]);
        }
    }

    /// <summary>
    /// Обработка сообщения при изменении любого поля на форме ввода личных данных.
    /// </summary>
    /// <param fullName="message"></param>
    public void Receive(ChangeFieldsPersonMessege message)
    {
        UpdateListFiles();
    }

    /// <summary>
    /// Вызов обновления списка документов
    /// </summary>
    private void UpdateListFiles()
    {
        string fullName = IsCheckedFilters ? PersonFormContent.FullName : string.Empty;
        Messenger.Send(new UpdateListFileMessage(fullName));
    }
}
