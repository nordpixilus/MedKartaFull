using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Messages;

internal class UpdateListFileMessage : ValueChangedMessage<string?>
{
    internal UpdateListFileMessage(string? value) : base(value) { }
}
