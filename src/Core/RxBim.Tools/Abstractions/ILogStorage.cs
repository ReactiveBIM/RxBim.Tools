namespace RxBim.Tools
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Service for storing different types of messages
    /// </summary>
    [PublicAPI]
    public interface ILogStorage
    {
        /// <summary>
        /// An event that is raised when a collection of storage items changes
        /// </summary>
        event Handlers.LogStorageChangedHandler ElementStorageChanged;

        /// <summary>
        /// Adds message to storage
        /// </summary>
        /// <param name="message">Message</param>
        /// <typeparam name="T">Message type</typeparam>
        public void AddMessage<T>(in T message)
            where T : ILogMessage;

        /// <summary>
        /// Returns message collection
        /// </summary>
        IEnumerable<ILogMessage> GetMessages();

        /// <summary>
        /// Return messages count
        /// </summary>
        int Count();

        /// <summary>
        /// Indicates that storage has messages
        /// </summary>
        bool HasMessages();

        /// <summary>
        /// Clears message storage
        /// </summary>
        void Clear();
    }
}