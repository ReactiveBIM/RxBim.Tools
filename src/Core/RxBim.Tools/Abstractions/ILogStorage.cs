namespace RxBim.Tools
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Представляет тип, который используется для ведения журнала ошибок.
    /// </summary>
    public interface ILogStorage
    {
        /// <summary>
        /// Событие, возникающие при изменении коллекции элементов хранилища.
        /// </summary>
        [UsedImplicitly]
        event EventHandler ElementStorageChanged;

        /// <summary>
        /// Уровень логирования
        /// </summary>
        [UsedImplicitly]
        public bool ShouldShowDebugMessages { get; set; }

        /// <summary>
        /// Добавить сообщение.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <typeparam name="T">Тип сообщения</typeparam>
        [UsedImplicitly]
        public void AddMessage<T>(in T message)
            where T : ILogMessage;

        /// <summary>
        /// Получить коллекцию ошибок.
        /// </summary>
        [UsedImplicitly]
        IEnumerable<ILogMessage> GetMessages();

        /// <summary>
        /// Указывает если ли ошибки в хранилище.
        /// </summary>
        [UsedImplicitly]
        bool HasMessages();

        /// <summary>
        /// Очищает хранилище.
        /// </summary>
        [UsedImplicitly]
        void Clear();
    }
}