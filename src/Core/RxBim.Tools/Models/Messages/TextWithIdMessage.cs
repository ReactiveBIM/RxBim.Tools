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

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return ToString().GetHashCode() ^ LogTime.ToString().GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is not TextWithIdMessage otherMessage)
                return false;
            return Text == otherMessage.Text && Equals(ObjectId, otherMessage.ObjectId) &&
                   LogTime.ToString() == otherMessage.LogTime.ToString();
        }
    }
}