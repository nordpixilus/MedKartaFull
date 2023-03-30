using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Shared.Navigations
{
    internal class NavigationMessage : ValueChangedMessage<string>
    {
        internal NavigationMessage(string value) : base(value) { }
    }
}