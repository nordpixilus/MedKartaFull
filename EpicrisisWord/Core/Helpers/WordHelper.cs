using System.Collections.Generic;
using System.IO;
using System.Windows;
using System;
using Word = Microsoft.Office.Interop.Word;
using static System.Environment;
using System.Threading;

namespace EpicrisisWord.Core.Helpers;

internal class WordHelper
{
    // Путь до файла шаблона
    //private string pathTemlateNameFile = Path.Combine("TemplatesWord", "epicrisis.docx");

    // Путь до до файла в папке документы
    //public readonly string specialFolderPathFile;

    //string shortFullName;

    // получаем название новаго файла
    //private readonly string newNameFile;

    private FileInfo? _fileInfoTemlate;

    private bool _isSave = false;



    private readonly Dictionary<string, string> fiedlsPerson;

    public WordHelper(Dictionary<string, string> fiedlsPerson)
    {
        this.fiedlsPerson = fiedlsPerson;
    }

    public void CreateEpicrisisFile()
    {
        _isSave = true;
        SetFileInfo("epicrisis");
        ProcessAdd();
    }

    public void CreateDiagnosisFile()
    {
        SetFileInfo("diagnosis");
        ProcessAdd();
    }

    private void SetFileInfo(string nameFile)
    {
        string pathTemlateNameFile = Path.Combine("TemplatesWord", $"{nameFile}.docx");
        _fileInfoTemlate = new FileInfo(pathTemlateNameFile);
    }

    private void ProcessAdd()
    {
        // Создаём объект документа
        Word.Document? doc = null;
        try
        {
            // Создаём объект приложения
            Word.Application app = new();

            // Путь до шаблона документа
            Object file = _fileInfoTemlate!.FullName;

            doc = app.Documents.Open(file);

            // Добавляем информацию
            // wBookmarks содержит все закладки
            Word.Bookmarks wBookmarks = doc.Bookmarks;
            Word.Range wRange;

            foreach (Word.Bookmark mark in wBookmarks)
            {
                //MessageBox.Show($"{mark.Name}");
                wRange = mark.Range;
                try
                {
                    wRange.Text = fiedlsPerson[mark.Name];
                }
                catch
                {
                    MessageBox.Show($"В шаблоне документа отсутствует ключ {mark.Name}");
                }
            }

            
            // Закрываем документ
            if (_isSave )
            {
                app.ActiveDocument.SaveAs2(fiedlsPerson["specialFolderPathFile"]);
            }
            else
            {
                app.Visible = true;
                app.ActiveDocument.Activate();
                Thread.Sleep(25000);
                //doc.Activate();
            }
            
            //app.ActiveDocument.Save();
            app.ActiveDocument.Close();
            doc = null;
            app.Quit();

        }
        catch (Exception)
        {
            MessageBox.Show("произошла ошибка");
            // Если произошла ошибка, то
            // закрываем документ и выводим информацию
            doc?.Close();
            doc = null;
        }
    }
}
// http://nullpro.info/2012/rabotaem-s-ms-word-iz-c-chast-1-otkryvaem-shablon-ishhem-tekst-vnutri-dokumenta/
// https://progtask.ru/rabota-s-word-pri-pomoshi-c-sharp/
// https://github.com/xceedsoftware/DocX