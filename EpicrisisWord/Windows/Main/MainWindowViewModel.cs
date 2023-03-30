using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Shared.Models;
using EpicrisisWord.Shared.Navigations;
using EpicrisisWord.Windows.Main.Views.Home;
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
            GoToHomeView();
        }

        [RelayCommand]
        private void GoToHomeView()
        {
            ChildContent = new HomeViewModel();
            Title = "Open HomeView";
        }

        [RelayCommand]
        private void GoToWorkView()
        {
            ChildContent = new WorkViewModel();
            Title = "Open WorkView";
        }

        public void Receive(NavigationMessage message)
        {
            switch (message.Value)
            {
                case nameof(WorkViewModel): GoToWorkView(); break;
                case nameof(HomeViewModel): GoToHomeView(); break;
            }
        }
    }
}