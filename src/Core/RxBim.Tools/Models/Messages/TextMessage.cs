namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    public class TextMessage : TextMessageBase
    {
        /// <inheritdoc />
        public TextMessage(string message, bool isDebugMessage = false)
            : base(message, isDebugMessage)
        {
        }
    }
}
