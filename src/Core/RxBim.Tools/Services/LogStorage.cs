namespace RxBim.Tools;

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

/// <inheritdoc />
[UsedImplicitly]
public class LogStorage : ILogStorage
{
    private readonly List<ILogMessage> _sourceItems = new();

    /// <inheritdoc />
    public event LogStorageMessageAddedEventHandler? MessageAdded;

    /// <inheritdoc />
    public event EventHandler? StorageCleared;

    /// <inheritdoc />
    public void AddMessage<T>(in T message)
        where T : ILogMessage
    {
        _sourceItems.Add(message);
        MessageAdded?.Invoke(this, new MessageAddedEventArgs(message));
    }

    /// <inheritdoc />
    public IEnumerable<ILogMessage> GetMessages()
    {
        return _sourceItems;
    }

    /// <inheritdoc />
    public int Count() => _sourceItems.Count;

    /// <inheritdoc />
    public bool HasMessages() => _sourceItems.Any();

    /// <inheritdoc />
    public void Clear()
    {
        _sourceItems.Clear();
        StorageCleared?.Invoke(this, EventArgs.Empty);
    }
}