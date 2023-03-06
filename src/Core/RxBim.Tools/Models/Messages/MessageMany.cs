namespace RxBim.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Базовый класс для набора сообщений с общим заголовком
    /// </summary>
    public class MessageMany : ILogMessage, ICanBeUnion<MessageMany>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageMany"/> class.
        /// </summary>
        /// <param name="messages">сообщения</param>
        /// <param name="isDebug">является ли сообщение отладочным</param>
        /// <param name="showMessageLogTime">показывать ли время записи сообщения</param>
        protected MessageMany(
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
        /// Заголовок
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Сообщения
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