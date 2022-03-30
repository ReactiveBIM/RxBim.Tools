namespace RxBim.Tools.TableBuilder.Abstractions
{
    using System.Collections.Generic;
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
        public (Table Data, List<string> Headers) Deserialize(T source);
    }
}