using System.Collections.Generic;
using System.IO;
using System.Windows;
using System;
using Word = Microsoft.Office.Interop.Word;
using static System.Environment;

namespace EpicrisisWord.Core.Helpers;

internal class WordHelper
{
    // получаем путь до файла шаблона
    private readonly string pathTemlateNameFile = Path.Combine("TemplatesWord", "epicrisis.docx");

    // путь до до файла в папке документы
    private readonly string specialFolderPathFile;

    //string shortFullName;

    // получаем название новаго файла
    private readonly string newNameFile;

    private readonly FileInfo _fileInfoTemlate;



    private readonly Dictionary<string, string> fiedlsPerson;

    public WordHelper(Dictionary<string, string> fiedlsPerson)
    {
        this.fiedlsPerson = fiedlsPerson;
        string shortFullName = RegexHelper.ExctraxtIni(fiedlsPerson["full_name"]);
        (newNameFile, specialFolderPathFile) = StringHelper.GetNewNameFile(shortFullName);

        _fileInfoTemlate = new FileInfo(pathTemlateNameFile);
    }

    public void ProcessAdd()
    {
        // Создаём объект документа
        Word.Document? doc = null;
        try
        {
            // Создаём объект приложения
            Word.Application app = new();

            // Путь до шаблона документа
            Object file = _fileInfoTemlate.FullName;

            // Открываем
            doc = app.Documents.Open(file);
            //doc.Activate();

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
                    MessageBox.Show($"В шалоне документа отсутствует ключ {mark.Name}");
                }
            }

            // Закрываем документ
            app.ActiveDocument.SaveAs2(specialFolderPathFile);
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