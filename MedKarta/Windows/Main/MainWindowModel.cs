using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MedKarta.Application;
using MedKarta.Core.Models;
using MedKarta.Shared.Navigations;
using MedKarta.Windows.Main.Views.Start;
using MedKarta.Windows.Main.Views.Work;
using Microsoft.Extensions.Logging;
using System;

namespace MedKarta.Windows.Main
{
    internal partial class MainWindowModel : BaseViewModel, IRecipient<NavigationMessage>
    {
        [ObservableProperty]
        private string? _Title;

        [ObservableProperty]
        private object? _ChildContent;
        private readonly IApp app;
        private readonly ILogger<MainWindowModel> logger;

        public MainWindowModel(IApp app, ILogger<MainWindowModel> logger)
        {
            this.app = app;
            this.logger = logger;
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this);
            GoToStartView();

        }

        [RelayCommand]
        private void GoToStartView()
        {
            ChangeChildContent<StartViewModel>();
            Title = "Open HomeView";
            logger.LogInformation("Переход на представление {title}", nameof(StartViewModel));
        }

        [RelayCommand]
        private void GoToWorkView()
        {
            ChangeChildContent<WorkViewModel>();
            Title = "Open WorkView";
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
    }
}