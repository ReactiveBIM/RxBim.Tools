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
        /// <param name="message">Message text</param>
        /// <param name="isDebugMessage">Indicates that message only exist in 'DEBUG' mode</param>
        protected MessageBase(string message, bool isDebugMessage = false)
        {
            IsDebugMessage = isDebugMessage;
            MessageLogTime = DateTime.Now;
            Text = message;
        }

        /// <inheritdoc />
        public string Text { get; }

        /// <summary>
        /// Indicates that message only exist in 'DEBUG' mode
        /// </summary>
        public bool IsDebugMessage { get; set; }

        /// <summary>
        /// Message log time
        /// </summary>
        public DateTime? MessageLogTime { get; }
    }
}