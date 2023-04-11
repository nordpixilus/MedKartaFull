using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EpicrisisWord.Core.Messages;

internal class ChangeFieldsPersonMessege : ValueChangedMessage<string>
{
    internal ChangeFieldsPersonMessege(string value) : base(value) { }
}
