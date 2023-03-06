namespace RxBim.Tools
{
    using System;

    /// <inheritdoc cref="TextIdMessageBase" />
    public class ErrorMessage : TextIdMessageBase, ICanBeUnion<ErrorMessage>
    {
        /// <inheritdoc />
        public ErrorMessage(string text, string element, IMessageData elementId, bool isDebug = false)
            : base(text, elementId, element, isDebug)
        {
        }

        /// <inheritdoc />
        public ILogMessage UnionMessages(ErrorMessage message)
        {
            if (!CanUnionWith(message))
            {
                throw new ArgumentException("Unable to merge messages");
            }

            return new ErrorMessageMany(new[] { this, message });
        }

        /// <inheritdoc />
        public bool CanUnionWith(ErrorMessage message)
        {
            return string.Equals(Text, message.Text);
        }
    }
}