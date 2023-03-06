namespace RxBim.Tools
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Представляет базовый тип для объекта сообщения журнала.
    /// </summary>
    public interface IMessageData : IComparable
    {
        /// <summary>
        /// Объект, используемый для описания.
        /// </summary>
        [UsedImplicitly]
        object DescriptionObject { get; }

        /// <summary>
        /// Возвращает идентификатор объекта.
        /// </summary>
        [UsedImplicitly]
        IObjectIdWrapper GetId();
    }
}
