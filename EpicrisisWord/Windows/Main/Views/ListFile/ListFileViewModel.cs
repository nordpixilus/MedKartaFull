using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;

namespace EpicrisisWord.Windows.Main.Views.ListFile;

internal partial class ListFileViewModel : BaseViewModel, IRecipient<UpdateListFileMessage>
{
    /// <summary>
    /// Заголовок списка файлов.
    /// </summary>
    [ObservableProperty]
    private string _HeaderText = string.Empty;

    /// <summary>
    /// Коллекция для отобращение в ListView
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<DocumentName> _Files = new();

    /// <summary>
    /// Выбранный документ в ListView. 
    /// </summary>
    [ObservableProperty]
    private DocumentName? _SelectedItemListViewFile;

    partial void OnSelectedItemListViewFileChanged(DocumentName? value)
    {
        if (value != null)
        {
            Messenger.Send(new CreateDocumentMessage(value.PathFile!));
        }
    }

    public ListFileViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        HeaderText = InitHeaderText();
    }

    private static string InitHeaderText()
    {
        if (string.IsNullOrEmpty(Properties.Settings.Default.work_folder))
        {
            return "Укажите папку с файлами";
        }
        else
        {
            return "Список файлов (Изменить папку)";
        }

    }

    public void UpdatePathWorkFolder(string path)
    {
        Properties.Settings.Default.work_folder = path;
        Properties.Settings.Default.Save();
        AddListPath(string.Empty);
        HeaderText = InitHeaderText();
    }

    public void Receive(UpdateListFileMessage? message)
    {
        if (message != null)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.work_folder))
            {
                AddListPath(message.Value!);
            }
        }
    }

    /// <summary>
    /// Создание списка файлов по совпадению с фамилией.
    /// </summary>
    private void AddListPath(string fullName)
    {
        string searchWord = string.Empty;
        string nameFile;

        Files.Clear();

        string patternFile = $"^.*(docx|odt)$";
        string patternType = $"^.*((Э|э)пикриз)|((С|с)татус).*$";
        string patternName;

        if (!string.IsNullOrEmpty(fullName))
        {
            searchWord = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            patternName = $"^[^~]*{searchWord}.*$";
        }
        else
        {
            patternName = $"^[^~]*.*$";
        }

        string[] pathFiles = Directory.GetFiles(Properties.Settings.Default.work_folder);

        foreach (string path in pathFiles)
        {
            nameFile = Path.GetFileName(path);
            if (Regex.IsMatch(nameFile, patternFile))
            {
                if (!Regex.IsMatch(nameFile, patternType))
                {
                    if (Regex.IsMatch(nameFile, patternName))
                    {
                        Files.Add(new DocumentName() { PathFile = path, NameFile = nameFile });
                    }
                }
            }
        }
    }
}