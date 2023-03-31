using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Messages;

internal class PathFileMessage : ValueChangedMessage<string>
{
    internal PathFileMessage(string value) : base(value) { }
}
