namespace RxBim.Tools
{
    using System;

    /// <summary>
    /// Базовый класс для всех сообщений
    /// </summary>
    public abstract class MessageBase : ILogMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class.
        /// </summary>
        /// <param name="isDebugMessage">является ли сообщение отладочным</param>
        /// <param name="showMessageLogTime">показывать ли время записи сообщения</param>
        protected MessageBase(bool isDebugMessage, bool showMessageLogTime = false)
        {
            IsDebugMessage = isDebugMessage;
            if (showMessageLogTime)
                MessageLogTime = DateTime.Now;
        }

        /// <inheritdoc />
        public bool IsDebugMessage { get; set; }

        /// <inheritdoc />
        public DateTime? MessageLogTime { get; }
    }
}