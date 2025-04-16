namespace RxBim.Tools;

using System;

/// <inheritdoc />
public abstract class MessageBase : ILogMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessageBase"/> class.
    /// </summary>
    /// <param name="message">Message text.</param>
    /// <param name="debugMessage">Indicates that message only exist in 'DEBUG' mode.</param>
    protected MessageBase(string message, bool debugMessage = false)
    {
        IsDebug = debugMessage;
        LogTime = DateTime.Now;
        Text = message;
    }

    /// <inheritdoc />
    public string Text { get; }

    /// <inheritdoc />
    public bool IsDebug { get; }

    /// <inheritdoc />
    public DateTime? LogTime { get; }
}