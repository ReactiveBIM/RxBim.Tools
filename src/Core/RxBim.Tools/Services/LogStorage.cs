namespace RxBim.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    public class LogStorage : ILogStorage
    {
        private readonly HashSet<ILogMessage> _sourceItems = new();

        /// <inheritdoc />
        public event EventHandler? ElementStorageChanged;

        /// <inheritdoc />
        public void AddTextMessage(string text)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void AddTextIdMessage(string text, IObjectIdWrapper id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void AddMessage<T>(in T message)
            where T : ILogMessage
        {
            if (_sourceItems.Add(message))
            {
                EventChanged();
            }
        }

        /// <inheritdoc />
        public IEnumerable<ILogMessage> GetMessages()
        {
            return _sourceItems;
        }

        /// <inheritdoc />
        public bool HasMessages() => _sourceItems.Any();

        /// <inheritdoc />
        public void Clear()
        {
            _sourceItems.Clear();
            EventChanged();
        }

        private void EventChanged() => ElementStorageChanged?.Invoke(this, null);
    }
}