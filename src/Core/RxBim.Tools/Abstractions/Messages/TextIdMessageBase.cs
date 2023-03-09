namespace RxBim.Tools
{
    /// <summary>
    /// Base class for text messages with element ids
    /// </summary>
    public abstract class TextIdMessageBase : MessageBase
    {
        /// <inheritdoc />
        protected TextIdMessageBase(string text, IObjectIdWrapper elementId, bool isDebugMessage = false)
            : base(text, isDebugMessage)
        {
            ElementId = elementId;
        }

        /// <summary>
        /// Element Id
        /// </summary>
        public IObjectIdWrapper ElementId { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Message} {"Id = " + ElementId}";
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
            return Message == other.Message && Equals(ElementId, other.ElementId);
        }
    }
}