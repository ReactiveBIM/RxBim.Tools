namespace RxBim.Tools
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    public class LogStorage : ILogStorage
    {
        private readonly HashSet<ILogMessage> _sourceItems = new();

        /// <inheritdoc />
        public event Handlers.LogStorageChangedHandler? ElementStorageChanged;

        /// <inheritdoc />
        public void AddMessage<T>(in T message)
            where T : ILogMessage
        {
            if (_sourceItems.Add(message))
            {
                ElementStorageChanged?.Invoke(this, new LogStorageChangedEventArgs(message));
            }
        }

        /// <inheritdoc />
        public IEnumerable<ILogMessage> GetMessages()
        {
            return _sourceItems;
        }

        /// <inheritdoc />
        public int Count() => _sourceItems.Count;

        /// <inheritdoc />
        public bool HasMessages() => _sourceItems.Any();

        /// <inheritdoc />
        public void Clear()
        {
            _sourceItems.Clear();
            ElementStorageChanged?.Invoke(this, new LogStorageChangedEventArgs(true));
        }
    }
}