namespace RxBim.Tools.TableBuilder.Models.Contents
{
    /// <summary>
    /// The content of a cell.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    public class CellContent<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellContent{T}"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public CellContent(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public T Value { get; }
    }
}