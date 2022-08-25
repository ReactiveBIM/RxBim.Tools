namespace RxBim.Tools.TableBuilder
{
    using JetBrains.Annotations;

    /// <summary>
    /// The numeric content of a cell
    /// </summary>
    [PublicAPI]
    public class NumericCellContent : ICellContent
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value">Number</param>
        /// <param name="format">Number format</param>
        public NumericCellContent(object value, string format)
        {
            Format = format;
            ValueObject = value;
        }

        /// <summary>
        /// Number format
        /// </summary>
        public string Format { get; set; }

        /// <inheritdoc />
        public object? ValueObject { get; }
    }
}