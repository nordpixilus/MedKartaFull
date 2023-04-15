using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MedKarta.Shared.Navigations
{
    internal class NavigationMessage : ValueChangedMessage<string>
    {
        public NavigationMessage(string value) : base(value) { }
    }
}