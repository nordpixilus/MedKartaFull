using System.Collections.Generic;
using System.IO;
using System.Windows;
using System;
using Word = Microsoft.Office.Interop.Word;
using static System.Environment;
using System.Threading;
using Microsoft.Office.Interop.Word;
using ICSharpCode.SharpZipLib.Core;

namespace EpicrisisWord.Core.Helpers;

internal class WordHelper
{
    private string? _fileInfoTemlate;

    private string? _fileInfoNew;


    private readonly Dictionary<string, string> fiedlsPerson;

    public WordHelper(Dictionary<string, string> fiedlsPerson)
    {
        this.fiedlsPerson = fiedlsPerson;
    }

    public void CreateWordFile(string nameFile)
    {
        _fileInfoTemlate = fiedlsPerson[$"path{nameFile}File"];
        _fileInfoNew = fiedlsPerson[$"pathNew{nameFile}File"];
        ProcessAdd();
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
            //Object file = _fileInfoTemlate!;

            doc = app.Documents.Open(_fileInfoTemlate);

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

            app.ActiveDocument.SaveAs2(_fileInfoNew);
            app.ActiveDocument.Close();
            doc = null;
            app.Quit();

        }
        catch (Exception ex)
        {
            MessageBox.Show($"произошла ошибка ProcessAdd {ex?.ToString()}");
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