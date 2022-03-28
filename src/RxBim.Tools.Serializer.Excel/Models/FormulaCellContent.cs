namespace RxBim.Tools.Serializer.Excel.Models
{
    using TableBuilder.Abstractions;

    /// <summary>
    /// Формула
    /// </summary>
    public class FormulaCellContent : ICellContent
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="formula">Формула</param>
        /// <param name="cellRange">Диапазон ячеек для формулы</param>
        public FormulaCellContent(
            Formulas formula,
            (int fromRow, int fromColumn, int toRow, int toColumn) cellRange)
        {
            Formula = formula;
            CellRange = cellRange;
        }

        /// <summary>
        /// Формула
        /// </summary>
        public Formulas Formula { get; set; }

        /// <summary>
        /// Диапазон ячеек для формулы
        /// </summary>
        public (int fromRow, int fromColumn, int toRow, int toColumn) CellRange { get; set; }

        /// <inheritdoc />
        public object? ValueObject => null;
    }
}