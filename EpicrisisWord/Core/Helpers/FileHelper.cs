using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System;

namespace EpicrisisWord.Core.Helpers;

internal static class FileHelper
{
    internal static void OpenDocumentWord(string path)
    {
        //MessageBox.Show(FiedlsPerson["full_name"]);
        //https://www.cyberforum.ru/windows-forms/thread2990748.html
        try
        {
            using Process process = new();
            process.StartInfo.UseShellExecute = true;
            //process.StartInfo.FileName = fiedlsPerson["path_file"];
            process.StartInfo.FileName = path;
            process.Start();
        }
        catch (Win32Exception noBrowser)
        {
            if (noBrowser.ErrorCode == -2147467259)
            {
                MessageBox.Show(noBrowser.Message);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
