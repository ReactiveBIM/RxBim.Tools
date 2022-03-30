namespace RxBim.Tools.Serializer.Excel.Models
{
    using TableBuilder.Abstractions;

    /// <summary>
    /// The numeric content of a cell
    /// </summary>
    public class NumerateCellContent : ICellContent
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value">Number</param>
        /// <param name="format">Number format</param>
        public NumerateCellContent(object value, string format)
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