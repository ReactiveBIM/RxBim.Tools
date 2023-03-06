namespace RxBim.Tools
{
    /// <summary>
    /// Base class for text messages with element ids
    /// </summary>
    public abstract class TextIdMessageBase : MessageBase
    {
        /// <inheritdoc />
        protected TextIdMessageBase(string text, IMessageData elementId, string element, bool isDebug)
            : base(isDebug, true)
        {
            Text = text;
            ElementId = elementId;
            Element = element;
        }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Element Id
        /// </summary>
        public IMessageData ElementId { get; }

        /// <summary>
        /// Element name
        /// </summary>
        public string Element { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Text} {Element} {"Id = " + ElementId}";
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
            return Equals((TextIdMessageBase)obj);
        }

        private bool Equals(TextIdMessageBase other)
        {
            return Text == other.Text && Equals(ElementId, other.ElementId) && Element == other.Element;
        }
    }
}