namespace RxBim.Tools
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>
    /// Extensions for <see cref="ILogStorage"/>.
    /// </summary>
    [PublicAPI]
    public static class LogStorageExtensions
    {
        /// <summary>
        /// Adds text message to <see cref="ILogStorage"/>
        /// </summary>
        /// <param name="logStorage"><see cref="ILogStorage"/> object.</param>
        /// <param name="text">Message text.</param>
        public static void AddTextMessage(this ILogStorage logStorage, string text)
        {
            logStorage.AddMessage(new TextMessage(text));
        }

        /// <summary>
        /// Adds text message with object Id to <see cref="ILogStorage"/>
        /// </summary>
        /// <param name="logStorage"><see cref="ILogStorage"/> object.</param>
        /// <param name="text">Message text.</param>
        /// <param name="id">Id of element.</param>
        public static void AddTextIdMessage(this ILogStorage logStorage, string text, IObjectIdWrapper id)
        {
            logStorage.AddMessage(new TextIdMessage(text, id));
        }

        /// <summary>
        /// Returns elements IDs combined by problem description
        /// </summary>
        /// <param name="logStorage"><see cref="ILogStorage"/> object.</param>
        public static IEnumerable<KeyValuePair<IObjectIdWrapper, string>> GetProblems(this ILogStorage logStorage)
        {
            var messages = logStorage.GetMessages();
            return messages
                .OfType<TextIdMessage>()
                .Select(mm => new KeyValuePair<IObjectIdWrapper, string>(
                    mm.ElementId,
                    mm.Text));
        }
    }
}