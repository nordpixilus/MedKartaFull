using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace EpicrisisWord.Windows.Main.Views.ListFile;

internal partial class ListFileViewModel : BaseViewModel, IRecipient<UpdateListFileMessage>
{
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
    }

    public void Receive(UpdateListFileMessage? message)
    {
        Files.Clear();
        if (message != null)
        {
            Files.Clear();
            AddListPath(message.Value!);
        }
    }

    /// <summary>
    /// Создание списка файлов по совпадению с фамилией.
    /// </summary>
    private void AddListPath(string fullName)
    {
        string searchWord = string.Empty;
        string nameFile;

        if (!string.IsNullOrEmpty(fullName))
        {            
            searchWord = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];                
        }

        string pattern = $"^[^~]*{searchWord}(?!.*((Э|э)пикриз)|(С|с)татус).*(docx|odt)$";

        string folderDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        string[] pathFiles = Directory.GetFiles(folderDocuments);        

        foreach (string path in pathFiles)
        {
            nameFile = Path.GetFileName(path);
            if(Regex.IsMatch(nameFile, pattern))
            {
                Files.Add(new DocumentName() { PathFile = path, NameFile = nameFile });
            }
        }
    }
}