using AODL.Document.Content;
using AODL.Document.TextDocuments;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace EpicrisisWord.Core.Helpers;

internal static class TextDocumentHelper
{
    static string textMessage = string.Empty;

    internal static string GetTextProblem(string pathFile)
    {
        textMessage = $"1) Закрыть документ.\n\n    {Path.GetFileName(pathFile)}\n\n2) Продожить выполнени: OK";

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
                MessageBox.Show(textMessage);
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
            MessageBox.Show(textMessage);
        }
        return textProblem;
    }
}
