using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System;

namespace EpicrisisWord.Core.Helpers;

internal static class DocumentHelper
{
    internal static void OpenDocumentToPath(string path)
    {
        //MessageBox.Show(FiedlsPerson["full_name"]);
        //https://www.cyberforum.ru/windows-forms/thread2990748.html
        //try
        //{
        //    using Process process = new();
        //    process.StartInfo.UseShellExecute = true;
        //    //process.StartInfo.FileName = fiedlsPerson["path_file"];
        //    process.StartInfo.FileName = path;
        //    process.Start();
        //}
        //catch (Win32Exception noBrowser)
        //{
        //    if (noBrowser.ErrorCode == -2147467259)
        //    {
        //        MessageBox.Show(noBrowser.Message);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //}

        // https://learn.microsoft.com/ru-ru/dotnet/api/system.diagnostics.process?view=net-7.0
        try
        {
            using Process myProcess = new();
            myProcess.StartInfo.UseShellExecute = true;
            // You can start any process, HelloWorld is a do-nothing example.
            myProcess.StartInfo.FileName = path;
            //myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself or you can do it programmatically
            // from this application using the Kill method.
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
