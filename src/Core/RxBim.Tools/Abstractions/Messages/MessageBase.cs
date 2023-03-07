namespace RxBim.Tools
{
    /// <summary>
    /// Base class for log messages
    /// </summary>
    public abstract class MessageBase : ILogMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class.
        /// </summary>
        /// <param name="message">Message text</param>
        protected MessageBase(string message)
        {
            Message = message;
        }

        /// <inheritdoc />
        public string Message { get; }
    }
}