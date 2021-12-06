namespace RxBim.Tools.TableBuilder.Abstractions
{
    using Models;

    /// <summary>
    /// Serializer of a <see cref="Table"/> object to object with type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The target type of a table.</typeparam>
    public interface ITableSerializer<out T>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="parameters">Serialization parameters.</param>
        T Serialize(Table table, params object[] parameters);
    }
}