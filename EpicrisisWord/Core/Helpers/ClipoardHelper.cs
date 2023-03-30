using System.Threading.Tasks;
using System.Windows;

namespace EpicrisisWord.Core.Helpers;

internal static class ClipoardHelper
{
    /// <summary>
    /// Получает в асинхронном режиме личные данные из буфера обмена.
    /// </summary>
    /// <returns>
    /// Возвращает srting или null.
    /// </returns>
    async internal static Task<string?> StartMoninitorFieldsPersonAsync()
    {
        string? fieldsText = null;
        bool x = true;
        while (x)
        {
            await Task.Delay(1000);
            fieldsText = GetFieldsPersonClipBoard();
            if (fieldsText != null)
            {                
                x = false;
            }
        }

        return fieldsText;
    }

    /// <summary>
    /// Получает личные данные из буфера обмена.
    /// </summary>
    /// <returns>
    /// Возвращает srting или null.
    /// </returns>
    internal static string? GetFieldsPersonClipBoard()
    {
        if (Clipboard.ContainsText() == true)
        {
            string? text = Clipboard.GetText();
            if (text.Contains("дата рождения"))
            {
                Clipboard.Clear();
                return text;
            }
        }

        return null;
    }
}
