using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MedKarta.Shared.Navigations;
using MedKarta.Windows.Main.Views.Work;

namespace MedKarta.Windows.Main.Views.Home
{
    internal partial class HomeViewModel : ObservableRecipient
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