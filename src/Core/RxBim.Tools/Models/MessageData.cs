namespace RxBim.Tools
{
    using System;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    public class MessageData : IMessageData
    {
        private readonly IObjectIdWrapper _id;
        private readonly string? _displayValue;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="MessageData"/>.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <param name="displayValue">Отображаемое значение.</param>
        public MessageData(IObjectIdWrapper id, string? displayValue = null)
        {
            DescriptionObject = id;
            _id = id;
            _displayValue = displayValue;
        }

        /// <inheritdoc />
        public object DescriptionObject { get; }

        /// <inheritdoc />
        public override string ToString() => (string.IsNullOrWhiteSpace(_displayValue)
                                                 ? _id.ToString()
                                                 : _displayValue)
                                             ?? string.Empty;

        /// <inheritdoc />
        public IObjectIdWrapper GetId() => _id;

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            return string.Compare(ToString(), obj.ToString(), StringComparison.Ordinal);
        }
    }
}