using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Navigations;

internal class NavigationMessage : ValueChangedMessage<string>
{
    internal NavigationMessage(string value) : base(value) { }
}