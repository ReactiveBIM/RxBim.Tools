namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    public class TextMessage : TextMessageBase
    {
        /// <inheritdoc />
        public TextMessage(string message)
            : base(message)
        {
        }
    }
}
