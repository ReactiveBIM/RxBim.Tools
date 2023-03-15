namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// EventArgs for <see cref="ILogStorage.ElementStorageChanged"/> event
/// </summary>
[PublicAPI]
public class LogStorageChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes insntance of <see cref="LogStorageChangedEventArgs"/>
    /// </summary>
    /// <param name="newMessage"><see cref="ILogMessage"/></param>
    public LogStorageChangedEventArgs(ILogMessage newMessage)
    {
        NewMessage = newMessage;
    }

    /// <summary>
    /// Initializes insntance of <see cref="LogStorageChangedEventArgs"/>
    /// </summary>
    /// <param name="storageCleared">Indicates that <see cref="ILogStorage"/> cleared</param>
    public LogStorageChangedEventArgs(bool storageCleared)
    {
        StorageCleared = storageCleared;
    }

    /// <summary>
    /// <see cref="ILogMessage"/>
    /// </summary>
    public ILogMessage? NewMessage { get; set; }

    /// <summary>
    /// Indicates that <see cref="ILogStorage"/> cleared
    /// </summary>
    public bool StorageCleared { get; set; }
}