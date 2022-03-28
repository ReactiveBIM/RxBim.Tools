namespace RxBim.Tools.Serializer.Excel.Models
{
    using TableBuilder.Abstractions;

    /// <summary>
    /// Numeric data
    /// </summary>
    public class NumerateCellContent : ICellContent
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value">Число</param>
        /// <param name="format">Формат числа</param>
        public NumerateCellContent(
            object value, string format)
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