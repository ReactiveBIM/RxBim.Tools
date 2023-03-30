namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// Common interface of log messages.
/// </summary>
[PublicAPI]
public interface ILogMessage
{
    /// <summary>
    /// Message text.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Indicates that message only exist in 'DEBUG' mode.
    /// </summary>
    public bool IsDebug { get; }

    /// <summary>
    /// Message log time.
    /// </summary>
    public DateTime? LogTime { get; }
}