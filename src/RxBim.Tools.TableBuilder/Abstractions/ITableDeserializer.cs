namespace RxBim.Tools.TableBuilder.Abstractions
{
    using Models;

    /// <summary>
    /// Table deserializer
    /// </summary>
    /// <typeparam name="T">Source type</typeparam>
    public interface ITableDeserializer<in T>
    {
        /// <summary>
        /// Deserializes a table from a data source
        /// </summary>
        /// <param name="source">Source data</param>
        public Table Deserialize(T source);
    }
}