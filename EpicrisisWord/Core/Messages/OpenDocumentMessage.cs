using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Messages;

internal class OpenDocumentMessage : ValueChangedMessage<string>
{
    internal OpenDocumentMessage(string value) : base(value) { }
}
