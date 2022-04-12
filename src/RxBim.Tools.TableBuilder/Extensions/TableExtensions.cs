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
        /// <param name="tableSerializer"><see cref="ITableSerializer{TParam, TTable}"/> object.</param>
        /// <param name="parameter">Serialization parameter.</param>
        /// <typeparam name="TParam">Serialization parameter type.</typeparam>
        /// <typeparam name="TResult">The target type of a table.</typeparam>
        public static TResult Serialize<TParam, TResult>(
            this Table table,
            ITableSerializer<TParam, TResult> tableSerializer,
            TParam parameter)
        {
            return tableSerializer.Serialize(table, parameter);
        }
    }
}