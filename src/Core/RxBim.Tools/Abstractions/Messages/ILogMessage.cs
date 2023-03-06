namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// Common interface of log messages
/// </summary>
public interface ILogMessage
{
    /// <summary>
    /// Indicates that message if for 'DEBUG' mode
    /// </summary>
    public bool IsDebugMessage { get; }

    /// <summary>
    /// Log time
    /// </summary>
    [UsedImplicitly]
    public DateTime? MessageLogTime { get; }
}