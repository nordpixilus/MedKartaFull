using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Shared.Models;
using EpicrisisWord.Shared.Navigations;
using EpicrisisWord.Windows.Main.Views.Work;

namespace EpicrisisWord.Windows.Main.Views.Home
{
    internal partial class HomeViewModel : BaseViewModel
    {
        public string TextLabel { get; set; } = "HomeView";
        public string TextButton { get; set; } = "GoToWorkView";

        [RelayCommand]
        private void GoToWorkView()
        {
            Messenger.Send(new NavigationMessage(nameof(WorkViewModel)));
            //WeakReferenceMessenger.Default.Send(new NavigationMessage(nameof(WorkViewModel)));
        }
    }
}