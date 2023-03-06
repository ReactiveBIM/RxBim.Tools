namespace RxBim.Tools
{
    /// <summary>
    /// A type for messages that can be union
    /// </summary>
    /// <typeparam name="T"><see cref="MessageBase"/>></typeparam>
    public interface ICanBeUnion<in T>
        where T : ILogMessage
    {
        /// <summary>
        /// Unions messages
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Возвращает объединенное сообщение</returns>
        public ILogMessage UnionMessages(T message);

        /// <summary>
        /// Checks that messages can be combined
        /// </summary>
        /// <param name="message">Сообщение</param>
        public bool CanUnionWith(T message);
    }
}
