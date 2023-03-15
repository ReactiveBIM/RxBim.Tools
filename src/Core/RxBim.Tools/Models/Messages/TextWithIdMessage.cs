namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <inheritdoc cref="TextWithIdMessageBase" />
    [UsedImplicitly]
    public class TextWithIdMessage : TextWithIdMessageBase
    {
        /// <inheritdoc />
        public TextWithIdMessage(string text, IObjectIdWrapper elementId, bool isDebugMessage = false)
            : base(text, elementId, isDebugMessage)
        {
        }
    }
}