using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MedKarta.Shared.Navigations;
using MedKarta.Windows.Main.Views.Start;

namespace MedKarta.Windows.Main.Views.Work
{
    internal partial class WorkViewModel : ObservableRecipient
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