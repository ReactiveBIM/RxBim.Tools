namespace RxBim.Tools.Helpers;

/// <summary>
/// Shared handlers
/// </summary>
public static class Handlers
{
    /// <summary>
    /// Handler for <see cref="ILogStorage"/> ElementStorageChanged event
    /// </summary>
    /// <param name="logMessage"><see cref="ILogMessage"/></param>
    /// <remarks>logMessage null value means that <see cref="ILogStorage"/> has
    /// no messages</remarks>
    public delegate void LogStorageChangedHandler(ILogMessage? logMessage);
}