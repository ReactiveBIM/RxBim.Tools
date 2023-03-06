namespace RxBim.Tools
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>
    /// List of <see cref="ErrorMessage"/> with common title
    /// </summary>
    internal class ErrorMessageMany : MessageMany, ICanBeUnion<ErrorMessage>
    {
        /// <inheritdoc />
        public ErrorMessageMany(IReadOnlyList<ErrorMessage> messages, bool isDebug = false)
            : base(messages, isDebug, true)
        {
            Title = messages.FirstOrDefault()?.Text ?? string.Empty;
        }

        /// <summary>
        /// Elements
        /// </summary>
        [UsedImplicitly]
        public List<UnionElements> ElementsList => Messages
            .Cast<ErrorMessage>()
            .GroupBy(m => m.Element)
            .Select(g => new UnionElements(
                g.Key,
                g.Select(m => m.ElementId)
                    .OrderBy(x => x)
                    .ToList()))
            .ToList();

        /// <inheritdoc />
        public ILogMessage UnionMessages(ErrorMessage message)
        {
            Messages.Add(message);
            IsDebugMessage = IsDebugMessage && message.IsDebugMessage;
            return this;
        }

        /// <inheritdoc />
        public bool CanUnionWith(ErrorMessage message)
        {
            return string.Equals(Title, message.Text);
        }
    }
}