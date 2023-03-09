namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <inheritdoc cref="TextIdMessageBase" />
    [UsedImplicitly]
    public class TextIdMessage : TextIdMessageBase
    {
        /// <inheritdoc />
        public TextIdMessage(string text, IObjectIdWrapper elementId, bool isDebugMessage = false)
            : base(text, elementId, isDebugMessage)
        {
        }
    }
}