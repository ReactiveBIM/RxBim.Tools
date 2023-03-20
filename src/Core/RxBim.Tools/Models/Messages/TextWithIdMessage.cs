namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <summary>
    /// Class for text messages with <see cref="IObjectIdWrapper"/>.
    /// </summary>
    [UsedImplicitly]
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
            return ToString().GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((TextWithIdMessage)obj);
        }

        private bool Equals(TextWithIdMessage other)
        {
            return Text == other.Text && Equals(ObjectId, other.ObjectId);
        }
    }
}