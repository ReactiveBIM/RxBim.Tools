namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Represents an interface of a table converter.
    /// </summary>
    /// <typeparam name="TSource">The type of the source table.</typeparam>
    /// <typeparam name="TParams">The type of the converter parameters.</typeparam>
    /// <typeparam name="TResult">The type of the conversion result.</typeparam>
    public interface ITableConverter<in TSource, in TParams, out TResult>
    {
        /// <summary>
        /// Converts a source table of <typeparamref name="TSource"/> type
        /// to a new object of <typeparamref name="TResult"/> type.
        /// </summary>
        /// <param name="table">
        /// The source table object.</param>
        /// <param name="parameters">The conversion parameters.</param>
        /// <returns>A new <typeparamref name="TResult"/> object.</returns>
        TResult Convert(TSource table, TParams parameters);
    }
}