namespace RxBim.Tools;

using JetBrains.Annotations;

/// <summary>
/// Extensions for <see cref="ILogStorage"/>.
/// </summary>
[PublicAPI]
public static class LogStorageExtensions
{
    /// <summary>
    /// Adds text message to <see cref="ILogStorage"/>.
    /// </summary>
    /// <param name="logStorage"><see cref="ILogStorage"/> object.</param>
    /// <param name="text">Message text.</param>
    public static void AddTextMessage(this ILogStorage logStorage, string text)
    {
        logStorage.AddMessage(new TextMessage(text));
    }

    /// <summary>
    /// Adds text message with object Id to <see cref="ILogStorage"/>.
    /// </summary>
    /// <param name="logStorage"><see cref="ILogStorage"/> object.</param>
    /// <param name="text">Message text.</param>
    /// <param name="id">Id of element.</param>
    public static void AddTextIdMessage(this ILogStorage logStorage, string text, IObjectIdWrapper id)
    {
        logStorage.AddMessage(new TextWithIdMessage(text, id));
    }
}