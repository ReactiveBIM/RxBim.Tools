namespace RxBim.Tools.TableBuilder.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the index of the position of the item in the collection.
        /// </summary>
        /// <param name="collection">Source collection.</param>
        /// <param name="item">Item from collection.</param>
        /// <typeparam name="T">Type of collection item.</typeparam>
        public static int IndexOf<T>(this IEnumerable<T> collection, T item)
            where T : class
        {
            return collection.TakeWhile(x => !item.Equals(x)).Count();
        }
    }
}