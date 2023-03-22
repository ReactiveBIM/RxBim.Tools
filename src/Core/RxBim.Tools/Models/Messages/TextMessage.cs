namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <summary>
    /// Class for text messages.
    /// </summary>
    [PublicAPI]
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
            if (obj is not TextMessage otherMessage)
                return false;
            return Text == otherMessage.Text && LogTime.ToString() == otherMessage.LogTime.ToString();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(Text)
                ? LogTime.ToString().GetHashCode()
                : Text.GetHashCode() ^ LogTime.ToString().GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Text;
        }
    }
}
