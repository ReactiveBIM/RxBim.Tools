namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <summary>
    /// Class for text messages.
    /// </summary>
    [UsedImplicitly]
    public class TextMessage : MessageBase
    {
        /// <inheritdoc />
        public TextMessage(string text, bool isDebugMessage = false)
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
            return Equals((TextMessage)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(Text) ? 0 : Text.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Text;
        }

        private bool Equals(TextMessage other)
        {
            return Text == other.Text;
        }
    }
}
