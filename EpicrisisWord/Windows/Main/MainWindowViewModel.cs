﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Helpers;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using EpicrisisWord.Windows.Main.Views.PersonForm;
using EpicrisisWord.Windows.Main.Views.Start;

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
        private void GoToPersonFormView(string? fieldsText)
        {
            if (fieldsText != null)
            {
                ChildContent = new PersonFormViewModel(fieldsText);
                Title = "Open WorkView";
            }
            
        }

        public void Receive(NavigationMessage message)
        {
            switch (message.Value)
            {
                case nameof(PersonFormViewModel): GoToPersonFormView(null); break;
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
                GoToPersonFormView(fieldsText);
            }
            else
            {
                GoToStartView();
            }
        }

        /// <summary>
        /// Получение и заполнение полей с личными данными.
        /// </summary>
        //private async void SetFiedsPersonAsync()
        //{
        //    (string text, bool isText) = ClipBoardMonitor.GetTextPersonClipBoard();
        //    if (isText)
        //    {
        //        ExctraxtFieldsPerson(text);

        //    }
        //    else
        //    {
        //        ExctraxtFieldsPerson(await ClipBoardMonitor.StartTextPerson());
        //    }
        //}

        #endregion
    }
}