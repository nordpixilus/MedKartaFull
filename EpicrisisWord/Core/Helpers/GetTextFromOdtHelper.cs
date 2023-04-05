using AODL.Document.Content;
using AODL.Document.TextDocuments;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace EpicrisisWord.Core.Helpers;

internal class GetTextFromOdtHelper
{
    internal static string GetTextFromOdt(string pathFile)
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
}
