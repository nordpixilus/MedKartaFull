using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Messages;

internal class ListFileMessage : ValueChangedMessage<string?>
{
    internal ListFileMessage(string? value) : base(value) { }
}
