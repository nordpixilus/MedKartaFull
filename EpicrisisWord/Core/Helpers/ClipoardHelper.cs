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
    /// Получает в асинхронном режиме текс проблем из буфера обмена.
    /// </summary>
    /// <returns>
    /// Возвращает srting или null.
    /// </returns>
    async internal static Task<string?> StartTextProblem()
    {
        string? text = null;
        bool x = true;
        while (x)
        {
            await Task.Delay(1000);
            if (Clipboard.ContainsText() == true)
            {
                text = Clipboard.GetText();
                Clipboard.Clear();
                x = false;
            }
        }

        return text;
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
            string? fieldsText = Clipboard.GetText();
            if (fieldsText.Contains("дата рождения:"))
            {
                Clipboard.Clear();
                return fieldsText;
            }
        }

        return null;
    }
}
