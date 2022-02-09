namespace RxBim.Tools.TableBuilder.Models.Contents
{
    using Abstractions;

    /// <summary>
    /// The content of a cell.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    public abstract class CellContent<T> : ICellContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellContent{T}"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        protected CellContent(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Cell content value.
        /// </summary>
        public T Value { get; }

        /// <inheritdoc />
        public object? ValueObject => Value;
    }
}