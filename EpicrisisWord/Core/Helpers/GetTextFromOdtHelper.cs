using AODL.Document.Content;
using AODL.Document.TextDocuments;
using System.Linq;
using System.Text;

namespace EpicrisisWord.Core.Helpers;

internal class GetTextFromOdtHelper
{
    internal static string GetTextFromOdt(string path)
    {
        var sb = new StringBuilder();
        using (var doc = new TextDocument())
        {
            try
            {
                doc.Load(path);
                //Верхний и нижний колонтитулы находятся в разделе "Стили документа". Возьмите XML-файл этой части
                //XElement stylesPart = XElement.Parse(doc.DocumentStyles.Styles.OuterXml);
                //Возьмите весь текст верхних и нижних колонтитулов, объединенный с возвращаемой кареткой
                //string stylesText = string.Join("\r\n", stylesPart.Descendants().Where(x => x.Name.LocalName == "header" || x.Name.LocalName == "footer").Select(y => y.Value));

                //Основной контент
                var mainPart = doc.Content.Cast<IContent>();
                var mainText = string.Join("\r\n", mainPart.Select(x => x.Node.InnerText));

                //Append both text variables
                //sb.Append(stylesText + "\r\n");
                sb.Append(mainText);
            }
            catch { }
        }

        return sb.ToString();
    }
}
