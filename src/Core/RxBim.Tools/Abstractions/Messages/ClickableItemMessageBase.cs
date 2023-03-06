namespace RxBim.Tools
{
    /// <summary>
    /// Базовый класс для элемента с кликабельных айди
    /// </summary>
    public abstract class ClickableItemMessageBase : MessageBase
    {
        /// <inheritdoc />
        protected ClickableItemMessageBase(string text, IMessageData elementId, string element, bool isDebug)
            : base(isDebug, true)
        {
            Text = text;
            ElementId = elementId;
            Element = element;
        }

        /// <summary>
        /// Текст.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Id элемента.
        /// </summary>
        public IMessageData ElementId { get; }

        /// <summary>
        /// Имя элемента.
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
            return 0;
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
            return Equals((ClickableItemMessageBase)obj);
        }

        private bool Equals(ClickableItemMessageBase other)
        {
            return Text == other.Text && Equals(ElementId, other.ElementId) && Element == other.Element;
        }
    }
}