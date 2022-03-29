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
        /// Deserialize table from source
        /// </summary>
        /// <param name="source">Source data</param>
        public (Table Data, List<string> Headers) DeserializeTable(T source);
    }
}