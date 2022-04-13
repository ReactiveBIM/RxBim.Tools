namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Represents an interface of a <see cref="Table"/> deserialazer.
    /// </summary>
    /// <typeparam name="TSource">The type of a source object.</typeparam>
    public interface ITableDeserializer<in TSource>
    {
        /// <summary>
        /// Creates a table instance from the given data source.
        /// </summary>
        /// <param name="source">The data source.</param>
        public Table Deserialize(TSource source);
    }
}