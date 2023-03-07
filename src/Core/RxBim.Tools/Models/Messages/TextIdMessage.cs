namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <inheritdoc cref="TextIdMessageBase" />
    [UsedImplicitly]
    public class TextIdMessage : TextIdMessageBase
    {
        /// <inheritdoc />
        public TextIdMessage(string text, IMessageData elementId)
            : base(text, elementId)
        {
        }
    }
}