namespace RxBim.Tools
{
    /// <inheritdoc />
    public class TextMessage : TextMessageBase
    {
        /// <inheritdoc />
        public TextMessage(string message, bool isDebug = false)
            : base(message, isDebug, true)
        {
        }
    }
}
