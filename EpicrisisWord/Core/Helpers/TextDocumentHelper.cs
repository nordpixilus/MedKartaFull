using AODL.Document.Content;
using AODL.Document.TextDocuments;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace EpicrisisWord.Core.Helpers;

internal static class TextDocumentHelper
{
    internal static string GetTextProblem(string pathFile)
    {
        string textProblem = string.Empty;

        // Получение расширения файла из пути к нему.
        string extensionFile = Path.GetExtension(pathFile).Remove(0, 1);

        switch (extensionFile)
        {
            case "odt":
                textProblem = GetTextFromOdt(pathFile);
                break;
            case "docx":
                textProblem = GetTextFromDocxHelper(pathFile);
                break;
        }

        return textProblem;
    }

    private static string GetTextFromOdt(string pathFile)
    {
        var sb = new StringBuilder();
        using (var doc = new TextDocument())
        {
            try
            {
                doc.Load(pathFile);
            }
            catch
            {
                MessageBox.Show($"Закрыть файл\n {Path.GetFileName(pathFile)}");

                doc.Load(pathFile);
            }

            var mainPart = doc.Content.Cast<IContent>();
            var mainText = string.Join("\r\n", mainPart.Select(x => x.Node.InnerText));
            sb.Append(mainText);
        }

        return sb.ToString();
    }

    // https://www.cyberforum.ru/csharp-net/thread450570.html
    private static string GetTextFromDocxHelper(string pathFile)
    {
        string textProblem = string.Empty;
        object Revert = false;
        object ReadOnly = true;
        Word.Application app = new();
        //object s = Word.Tasks.Count;
        //Object fileName = dialog.FileName;
        try
        {
            app.Documents.Open(pathFile, ref Revert, ref ReadOnly);
            Word.Document doc = app.ActiveDocument;
            // Нумерация параграфов начинается с одного
            for (int i = 1; i < doc.Paragraphs.Count; i++)
            {
                textProblem += doc.Paragraphs[i].Range.Text;

            }
            app.Quit();

        }
        catch
        {
            MessageBox.Show($"Закрыть Word документ. {Path.GetFileName(pathFile)}");
        }
        return textProblem;
    }
}
