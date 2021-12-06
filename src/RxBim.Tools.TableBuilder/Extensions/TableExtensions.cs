namespace RxBim.Tools.TableBuilder.Extensions
{
    using Abstractions;
    using Models;

    /// <summary>
    /// Extensions for <see cref="Table"/>
    /// </summary>
    public static class TableExtensions
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> object to another type of table.
        /// </summary>
        /// <param name="table">Source <see cref="Table"/> object.</param>
        /// <param name="tableSerializer"><see cref="ITableSerializer{T}"/> object.</param>
        /// <param name="parameters">Serialization parameters.</param>
        /// <typeparam name="T">The target type of a table.</typeparam>
        public static T Serialize<T>(
            this Table table,
            ITableSerializer<T> tableSerializer,
            params object[] parameters)
        {
            return tableSerializer.Serialize(table, parameters);
        }
    }
}