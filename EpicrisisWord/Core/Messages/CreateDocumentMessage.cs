using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Messages;

internal class CreateDocumentMessage : ValueChangedMessage<string>
{
    internal CreateDocumentMessage(string value) : base(value) { }
}
