﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace EpicrisisWord.Shared.Models;

internal abstract class BaseViewModel : ObservableValidator
{
    protected BaseViewModel() : this(WeakReferenceMessenger.Default) { }

    protected BaseViewModel(IMessenger messenger)
    {
        Messenger = messenger;
    }

    protected IMessenger Messenger { get; }
}
