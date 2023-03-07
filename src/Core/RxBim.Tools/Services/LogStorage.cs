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
        public bool ShouldShowDebugMessages { get; set; }

        /// <inheritdoc />
        public void AddMessage<T>(in T message)
            where T : ILogMessage
        {
            var baseMessage = message;
            if (TryGetMessageToUnion(message, out var resMessage))
            {
                if (resMessage != null)
                {
                    _sourceItems.Add(resMessage.UnionMessages(message));
                    EventChanged();
                }
            }
            else
            {
                if (_sourceItems.Add(baseMessage))
                {
                    EventChanged();
                }
            }
        }

        /// <inheritdoc />
        public IEnumerable<ILogMessage> GetMessages()
        {
            return _sourceItems.Where(FilterDebugMessages);
        }

        /// <inheritdoc />
        public bool HasMessages() => _sourceItems.Where(FilterDebugMessages).Any();

        /// <inheritdoc />
        public void Clear()
        {
            _sourceItems.Clear();
            EventChanged();
        }

        private bool TryGetMessageToUnion<T>(T message, out ICanBeUnion<T>? outMessage)
            where T : ILogMessage
        {
            foreach (var baseMessage in _sourceItems)
            {
                if (baseMessage is ICanBeUnion<T> parent && parent.CanUnionWith(message))
                {
                    _sourceItems.Remove(baseMessage);
                    outMessage = parent;
                    return true;
                }
            }

            outMessage = message as ICanBeUnion<T>;
            return false;
        }

        private void EventChanged() => ElementStorageChanged?.Invoke(this, null);

        private bool FilterDebugMessages(ILogMessage arg)
        {
            return ShouldShowDebugMessages || !arg.IsDebugMessage;
        }
    }
}