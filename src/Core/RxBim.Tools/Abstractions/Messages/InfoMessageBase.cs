namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <summary>
    /// Базовый класс для информационного сообщения
    /// </summary>
    [UsedImplicitly]
    public abstract class InfoMessageBase : MessageBase
    {
        private readonly string _message;

        /// <inheritdoc />
        protected InfoMessageBase(string message, bool isDebug, bool showMessageLogTime = false)
            : base(isDebug, showMessageLogTime)
        {
            _message = message;
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
            return Equals((InfoMessageBase)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(_message) ? 0 : _message.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _message;
        }

        private bool Equals(InfoMessageBase other)
        {
            return _message == other._message;
        }
    }
}