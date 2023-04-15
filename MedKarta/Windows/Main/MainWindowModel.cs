﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MedKarta.Core;
using MedKarta.Core.Models;
using MedKarta.Core.Navigations;
using MedKarta.UCL.ErrorBoard.ErrorKod;
using MedKarta.UCL.Models;
using MedKarta.UCL.Start;
using MedKarta.UCL.Work;
using Microsoft.Extensions.Logging;
using System;

namespace MedKarta.Windows.Main
{
    internal partial class MainWindowModel : BaseViewModel, IRecipient<NavigationMessage>
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
        private Object? _ChildContent;

        #endregion

        

        private readonly IApp app;
        private readonly ILogger<MainWindowModel> logger;

        public PersonModel personModel { get; }

        public MainWindowModel(IApp app, ILogger<MainWindowModel> logger, PersonModel model)
        {
            this.app = app;
            this.logger = logger;
            personModel = model;
            SetView();
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this);
            //PersonModel.
            //SetFiedsPerson();

        }

        private void SetView()
        {
            if (!personModel.IsClipBoardText())
            {
                ChangeChildContent<StartViewModel>();
            }
            else if (!string.IsNullOrEmpty(personModel.IsErrorPersonWork()))
            {
                ErrorCorrectionPersonWork();
            }
            else if (personModel.IsPersonDB())
            {
                EquatePersonWorkToDB();
            }
            else
            {
                // TODO: начало нового приёма
                ChangeChildContent<StartViewModel>();
            }

            //if (personModel.CurrentView == nameof(ErrorKodViewModel))
            //{
            //    ChangeChildContent<ErrorKodViewModel>();
            //}
            //else
            //{
            //    ChangeChildContent<StartViewModel>();
            //}
        }

        private void EquatePersonWorkToDB()
        {
            // TODO реализовать проверку
            //personModel.EquatePerson();
            ChangeChildContent<StartViewModel>();
        }

        private void ErrorCorrectionPersonWork()
        {
            // TODO: реализовать проверку.
            ChangeChildContent<ErrorKodViewModel>();
        }

        [RelayCommand]
        private void GoToStartView()
        {
            ChangeChildContent<StartViewModel>();
            logger.LogInformation("Переход на представление {title}", nameof(StartViewModel));
        }

        [RelayCommand]
        private void GoToWorkView()
        {
            ChangeChildContent<WorkViewModel>();
            logger.LogInformation("Переход на представление {title}", nameof(WorkViewModel));
        }

        public void Receive(NavigationMessage message)
        {

            switch (message.Value)
            {
                case nameof(WorkViewModel): GoToWorkView(); break;
                case nameof(StartViewModel): GoToStartView(); break;
            }
        }

        private void ChangeChildContent<T>() where T : BaseViewModel
        {
            try
            {
                ChildContent = app.GetViewModel<T>().View;
            }
            catch (ArgumentNullException ex)
            {
                logger.LogCritical("{ex} {dop_text}", ex.Message, $"Не найдено представление для {typeof(T)}");
            }
        }

        #region Логика переключения представления в главном окне.

        /// <summary>
        /// Логика переключения представления в главном окне.
        /// </summary>
        //private void SetFiedsPerson()
        //{
        //    string? fieldsText = ClipBoard.GetFieldsPersonClipBoard();
        //    if (fieldsText != null)
        //    {
        //        //GoToWorkView(fieldsText);
        //    }
        //    else
        //    {
        //        GoToStartView();
        //        SetFiedsPersonAsync();
        //    }
        //}


        //private async void SetFiedsPersonAsync()
        //{
        //    string? fieldsText = await ClipBoard.StartMoninitorFieldsPersonAsync();
        //    if (fieldsText != null)
        //    {
        //        //GoToWorkView(fieldsText);
        //    }
        //}

        #endregion
    }
}