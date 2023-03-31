using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using EpicrisisWord.Windows.Main.Views.Start;
using EpicrisisWord.Windows.Main.Views.Work;

namespace EpicrisisWord.Windows.Main
{
    internal partial class MainWindowViewModel : BaseViewModel, IRecipient<NavigationMessage>
    {
        [ObservableProperty]
        private string? _Title;

        [ObservableProperty]
        private BaseViewModel? _ChildContent;

        public MainWindowViewModel()
        {
            WeakReferenceMessenger.Default.Register(this);
            SetFiedsPerson();
        }

        #region Команды переключения главного представления

        [RelayCommand]
        private void GoToStartView()
        {
            ChildContent = new StartViewModel();
            Title = "Open HomeView";
        }

        [RelayCommand]
        private void GoToWorkView(string? fieldsText)
        {
            if (fieldsText != null)
            {
                ChildContent = new WorkViewModel(fieldsText);
                Title = "Open WorkView";
            }
            
        }

        public void Receive(NavigationMessage message)
        {
            switch (message.Value)
            {
                case nameof(WorkViewModel): GoToWorkView(null); break;
                case nameof(StartViewModel): GoToStartView(); break;
            }
        }

        #endregion

        #region

        private void SetFiedsPerson()
        {
            string? fieldsText = ClipoardHelper.GetFieldsPersonClipBoard();
            if (fieldsText != null)
            {
                GoToWorkView(fieldsText);
            }
            else
            {
                GoToStartView();
                SetFiedsPersonAsync();
            }
        }

       
        private async void SetFiedsPersonAsync()
        {
            string? fieldsText = await ClipoardHelper.StartMoninitorFieldsPersonAsync();
            if (fieldsText != null)
            {
                GoToWorkView(fieldsText);
            }
        }

        #endregion
    }
}