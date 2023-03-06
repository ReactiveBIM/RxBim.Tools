namespace RxBim.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>
    /// Base class for list of messages witch common title
    /// </summary>
    public class MessageMany : ILogMessage, ICanBeUnion<MessageMany>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageMany"/> class.
        /// </summary>
        /// <param name="messages">Messages</param>
        /// <param name="isDebug">Indicates that message if for 'DEBUG' mode</param>
        /// <param name="showMessageLogTime">Indicates that the time should be recorded</param>
        [UsedImplicitly]
        public MessageMany(
            IReadOnlyList<ILogMessage> messages,
            bool isDebug = false,
            bool showMessageLogTime = false)
        {
            Messages = messages.ToList();
            IsDebugMessage = isDebug;
            if (showMessageLogTime)
                MessageLogTime = DateTime.Now;
        }

        /// <inheritdoc />
        public bool IsDebugMessage { get; set; }

        /// <inheritdoc />
        public DateTime? MessageLogTime { get; }

        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Messages
        /// </summary>
        public List<ILogMessage> Messages { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            foreach (var message in Messages)
            {
                sb.AppendLine(message.ToString());
            }

            return sb.ToString();
        }

        /// <inheritdoc />
        public ILogMessage UnionMessages(MessageMany message)
        {
            Messages.Add(message);
            return this;
        }

        /// <inheritdoc />
        public bool CanUnionWith(MessageMany message)
        {
            return string.Equals(Title, message.Title);
        }
    }
}