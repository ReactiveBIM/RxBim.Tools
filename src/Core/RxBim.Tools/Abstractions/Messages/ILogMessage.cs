namespace RxBim.Tools;

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
}