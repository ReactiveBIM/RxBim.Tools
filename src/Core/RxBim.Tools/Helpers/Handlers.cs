namespace RxBim.Tools;

/// <summary>
/// Shared handlers
/// </summary>
public static class Handlers
{
    /// <summary>
    /// Handler for <see cref="ILogStorage.ElementStorageChanged"/> event
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="args"><see cref="LogStorageChangedEventArgs"/></param>
    public delegate void LogStorageChangedHandler(object sender, LogStorageChangedEventArgs args);
}