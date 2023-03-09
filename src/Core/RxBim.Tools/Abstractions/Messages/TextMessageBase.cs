namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <summary>
    /// Base class for text message
    /// </summary>
    [UsedImplicitly]
    public abstract class TextMessageBase : MessageBase
    {
        /// <inheritdoc />
        protected TextMessageBase(string text, bool isDebugMessage = false)
            : base(text, isDebugMessage)
        {
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
            return Equals((TextMessageBase)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(Message) ? 0 : Message.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Message;
        }

        private bool Equals(TextMessageBase other)
        {
            return Message == other.Message;
        }
    }
}