using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Messages;

internal class ChangeFieldBlockMessege : ValueChangedMessage<string>
{
    internal ChangeFieldBlockMessege(string value) : base(value) { }
}
