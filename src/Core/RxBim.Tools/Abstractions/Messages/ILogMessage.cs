namespace RxBim.Tools;

using System;
using JetBrains.Annotations;

/// <summary>
/// Общий интерфейс сообщения лога
/// </summary>
public interface ILogMessage
{
    /// <summary>
    /// Указывает, является ли сообщение отладочным
    /// </summary>
    public bool IsDebugMessage { get; }

    /// <summary>
    /// Время записи сообщения
    /// </summary>
    [UsedImplicitly]
    public DateTime? MessageLogTime { get; }
}