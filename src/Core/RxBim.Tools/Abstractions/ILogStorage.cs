namespace RxBim.Tools
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Represents a type that is used for error logging.
    /// </summary>
    public interface ILogStorage
    {
        /// <summary>
        /// An event that is raised when a collection of storage items changes.
        /// </summary>
        [UsedImplicitly]
        event EventHandler ElementStorageChanged;

        /// <summary>
        /// Indicates that <see cref="GetMessages"/> will return 'DEBUG' messages
        /// </summary>
        [UsedImplicitly]
        public bool ShouldShowDebugMessages { get; set; }

        /// <summary>
        /// Adds message to storage
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <typeparam name="T">Тип сообщения</typeparam>
        [UsedImplicitly]
        public void AddMessage<T>(in T message)
            where T : ILogMessage;

        /// <summary>
        /// Returns message collection
        /// </summary>
        [UsedImplicitly]
        IEnumerable<ILogMessage> GetMessages();

        /// <summary>
        /// Indicates that storage has messages
        /// </summary>
        [UsedImplicitly]
        bool HasMessages();

        /// <summary>
        /// Clears message storage
        /// </summary>
        [UsedImplicitly]
        void Clear();
    }
}