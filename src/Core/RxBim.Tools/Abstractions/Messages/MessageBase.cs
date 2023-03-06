namespace RxBim.Tools
{
    using System;

    /// <summary>
    /// Base class for log messages
    /// </summary>
    public abstract class MessageBase : ILogMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class.
        /// </summary>
        /// <param name="isDebugMessage">Indicates that message if for 'DEBUG' mode</param>
        /// <param name="showMessageLogTime">Indicates that the time should be recorded</param>
        protected MessageBase(bool isDebugMessage, bool showMessageLogTime = false)
        {
            IsDebugMessage = isDebugMessage;
            if (showMessageLogTime)
                MessageLogTime = DateTime.Now;
        }

        /// <inheritdoc />
        public bool IsDebugMessage { get; }

        /// <inheritdoc />
        public DateTime? MessageLogTime { get; }
    }
}