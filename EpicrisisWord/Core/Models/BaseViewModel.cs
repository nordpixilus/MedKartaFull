using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace EpicrisisWord.Core.Models;

// https://github.com/CommunityToolkit/WindowsCommunityToolkit/issues/3804
internal abstract class BaseViewModel : ObservableValidator
{
    protected BaseViewModel() : this(WeakReferenceMessenger.Default) { }

    protected BaseViewModel(IMessenger messenger)
    {
        Messenger = messenger;
    }

    protected IMessenger Messenger { get; }
}
