namespace RxBim.Tools.TableBuilder.Abstractions
{
    using Models;

    /// <summary>
    /// Represents an interface of a <see cref="Table"/> serialazer.
    /// </summary>
    /// <typeparam name="TParams">The type of the serializer parameters.</typeparam>
    /// <typeparam name="TResult">The type of the serialization result.</typeparam>
    public interface ITableSerializer<in TParams, out TResult>
    {
        /// <summary>
        /// Serializes a <see cref="Table"/> instance.
        /// </summary>
        /// <param name="table">
        /// The source <see cref="Table"/> object.</param>
        /// <param name="parameters">The serialization parameters.</param>
        TResult Serialize(Table table, TParams parameters);
    }
}