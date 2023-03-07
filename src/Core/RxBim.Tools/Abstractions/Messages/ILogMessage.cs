namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// Common interface of log messages
/// </summary>
public interface ILogMessage
{
    /// <summary>
    /// Message text
    /// </summary>
    [UsedImplicitly]
    public string Message { get; }

    /// <summary>
    /// Indicates that message only exist in 'DEBUG' mode
    /// </summary>
    [UsedImplicitly]
    public bool IsDebugMessage { get; set; }

    /// <summary>
    /// Message log time
    /// </summary>
    [UsedImplicitly]
    public DateTime? MessageLogTime { get; }
}