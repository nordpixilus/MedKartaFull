using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EpicrisisWord.Core.Models;
using EpicrisisWord.Core.Navigations;
using EpicrisisWord.Windows.Main.Views.Start;

namespace EpicrisisWord.Windows.Main.Views.Work
{
    internal partial class WorkViewModel : BaseViewModel
    {
        public string TextLabel { get; set; } = "WorkView";
        public string TextButton { get; set; } = "GoToHomeView";

        [RelayCommand]
        private void GoToHomeView()
        {
            Messenger.Send(new NavigationMessage(nameof(StartViewModel)));
            //WeakReferenceMessenger.Default.Send(new NavigationMessage(nameof(HomeViewModel)));
        }
    }
}