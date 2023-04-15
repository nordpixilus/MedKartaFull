using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MedKarta.Application;
using MedKarta.Shared.Navigations;
using MedKarta.Windows.Main.Views.Home;
using MedKarta.Windows.Main.Views.Work;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace MedKarta.Windows.Main
{
    internal partial class MainWindowModel : ObservableRecipient, IRecipient<NavigationMessage>
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
            this.IsActive = true;
            GoToHomeView();

        }

        [RelayCommand]
        private void GoToHomeView()
        {
            ChangeChildContent<HomeViewModel>();
            Title = "Open HomeView";
            logger.LogInformation("Переход на представление {title}", nameof(HomeViewModel));
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
                case nameof(HomeViewModel): GoToHomeView(); break;
            }
        }

        private void ChangeChildContent<T>() where T : class
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