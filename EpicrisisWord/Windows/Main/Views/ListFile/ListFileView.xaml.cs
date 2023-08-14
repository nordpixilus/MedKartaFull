using System;
using System.Windows;
using System.Windows.Controls;
using WinWorms = System.Windows.Forms;

namespace EpicrisisWord.Windows.Main.Views.ListFile
{
    /// <summary>
    /// Логика взаимодействия для ListFileView.xaml
    /// </summary>
    public partial class ListFileView : UserControl
    {
        public ListFileView()
        {
            InitializeComponent();
        }

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            WinWorms.FolderBrowserDialog dialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            WinWorms.DialogResult result = dialog.ShowDialog();

            if (result == WinWorms.DialogResult.OK)
            {
                if (sender is ListView listView)
                {
                    ListFileViewModel vm = (ListFileViewModel)listView.DataContext;
                    vm.UpdatePathWorkFolder(dialog.SelectedPath);
                }
            }
        }
    }
}
