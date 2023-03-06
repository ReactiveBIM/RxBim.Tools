namespace RxBim.Tools
{
    /// <summary>
    /// Позволяет объединиться
    /// </summary>
    /// <typeparam name="T"><see cref="MessageBase"/>></typeparam>
    public interface ICanBeUnion<in T>
        where T : ILogMessage
    {
        /// <summary>
        /// Объединить сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Возвращает объединенное сообщение</returns>
        public ILogMessage UnionMessages(T message);

        /// <summary>
        /// Проверка, можно ли объединить с заданным сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public bool CanUnionWith(T message);
    }
}
