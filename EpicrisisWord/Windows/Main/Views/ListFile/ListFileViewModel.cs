using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Messages;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Documents;

namespace EpicrisisWord.Windows.Main.Views.ListFile;

internal partial class ListFileViewModel : BaseViewModel, IRecipient<ListFileMessage>
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

    //partial void OnSelectedItemListViewFileChanged(DocumentName? value)
    //{
    //    if (value != null)
    //    {
    //        //FileHelper.OpenDocumentWord(value.PathFile!);
    //        //GetTextProblemClipBoard();

    //        // послать сообщение
    //    }
    //}

    public ListFileViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(ListFileMessage? message)
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
        string folderDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string[] pathFiles = Directory.GetFiles(folderDocuments);
        string family = fullName.Split(' ')[0];

        foreach (string file in pathFiles)
        {
            if (RegexHelper.IsFamilyToPathFile(file, family))
            {
                Files.Add(new DocumentName() { PathFile = file, NameFile = Path.GetFileName(file) });
            }
        }

    }


}