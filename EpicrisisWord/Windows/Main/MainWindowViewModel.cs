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
        #region Title
        [ObservableProperty]
        private string? _Title = "Эпикриз";
        #endregion

        #region ChildContent

        /// <summary>
        /// Содержит программно изменяемый контент главного окна.
        /// </summary>
        [ObservableProperty]
        private BaseViewModel? _ChildContent;

        #endregion

        public MainWindowViewModel()
        {
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this);
            //WeakReferenceMessenger.Default.RegisterAll(this);
            SetFiedsPerson();
        }

        #region Команды переключения главного представления

        /// <summary>
        /// Переключение на стартовое окно
        /// </summary>
        [RelayCommand]
        private void GoToStartView()
        {
            ChildContent = new StartViewModel();
        }

        /// <summary>
        /// Переключение на окно ввода информации.
        /// </summary>
        /// <param name="fieldsText">Текст содержащий личные данные пациента.</param>
        [RelayCommand]
        private void GoToWorkView(string? fieldsText)
        {
            if (fieldsText != null)
            {
                ChildContent = new WorkViewModel(fieldsText);
            }
            
        }

        #endregion

        #region Обработка сообщения

        public void Receive(NavigationMessage message)
        {
            switch (message.Value)
            {
                case nameof(WorkViewModel): GoToWorkView(null); break;
                case nameof(StartViewModel): GoToStartView(); break;
            }
        }

        #endregion

        #region Логика переключения представления в главном окне.

        /// <summary>
        /// Логика переключения представления в главном окне.
        /// </summary>
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