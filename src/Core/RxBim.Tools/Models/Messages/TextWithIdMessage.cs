namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <summary>
    /// Class for text messages with <see cref="IObjectIdWrapper"/>.
    /// </summary>
    [PublicAPI]
    public class TextWithIdMessage : MessageBase
    {
        /// <inheritdoc />
        public TextWithIdMessage(string text, IObjectIdWrapper objectId, bool isDebugMessage = false)
            : base(text, isDebugMessage)
        {
            ObjectId = objectId;
        }

        /// <summary>
        /// Object Id.
        /// </summary>
        public IObjectIdWrapper ObjectId { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Text} ({ObjectId})";
        }
    }
}