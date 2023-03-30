using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Shared.Models;
using EpicrisisWord.Shared.Navigations;
using EpicrisisWord.Windows.Main.Views.Home;

namespace EpicrisisWord.Windows.Main.Views.Work
{
    internal partial class WorkViewModel : BaseViewModel
    {
        public string TextLabel { get; set; } = "WorkView";
        public string TextButton { get; set; } = "GoToHomeView";

        [RelayCommand]
        private void GoToHomeView()
        {
            Messenger.Send(new NavigationMessage(nameof(HomeViewModel)));
            //WeakReferenceMessenger.Default.Send(new NavigationMessage(nameof(HomeViewModel)));
        }
    }
}