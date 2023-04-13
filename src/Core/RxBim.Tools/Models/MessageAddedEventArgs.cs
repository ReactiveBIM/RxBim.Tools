namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// EventArgs for <see cref="ILogStorage.MessageAdded"/> event.
/// </summary>
[PublicAPI]
public class MessageAddedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes instance of <see cref="MessageAddedEventArgs"/>.
    /// </summary>
    /// <param name="newMessage"><see cref="ILogMessage"/>.</param>
    public MessageAddedEventArgs(ILogMessage newMessage)
    {
        NewMessage = newMessage;
    }

    /// <summary>
    /// <see cref="ILogMessage"/>.
    /// </summary>
    public ILogMessage NewMessage { get; set; }
}