using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System;

namespace EpicrisisWord.Core.Helpers;

internal static class DocumentHelper
{
    internal static void OpenDocumentToPath(string path)
    {
        //https://www.cyberforum.ru/windows-forms/thread2990748.html

        // https://learn.microsoft.com/ru-ru/dotnet/api/system.diagnostics.process?view=net-7.0
        try
        {
            using Process myProcess = new();

            myProcess.StartInfo.UseShellExecute = true;
            // You can start any process, HelloWorld is a do-nothing example.
            myProcess.StartInfo.FileName = path;
            //myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
            // Этот код предполагает, что процесс, который вы запускаете, завершится сам по себе.
            // Учитывая, что он запущен без окна, поэтому вы не можете его завершить
            // на рабочем столе он должен завершиться сам по себе, или вы можете сделать это программно
            // из этого приложения с использованием метода Kill.
        }
        catch
        {
            MessageBox.Show(@"Ошибка OpenDocumentToPath.");
        }
    }
}
