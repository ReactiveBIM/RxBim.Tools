namespace RxBim.Tools.TableBuilder
{
    using JetBrains.Annotations;

    /// <summary>
    /// The formula content of a cell
    /// </summary>
    [PublicAPI]
    public class FormulaCellContent : ICellContent
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="formula">Formulas</param>
        /// <param name="cellRange">Cell range for formula</param>
        public FormulaCellContent(
            Formulas formula,
            (int FromRow, int FromColumn, int ToRow, int ToColumn) cellRange)
        {
            Formula = formula;
            CellRange = cellRange;
        }

        /// <summary>
        /// Formulas
        /// </summary>
        public Formulas Formula { get; set; }

        /// <summary>
        /// Cell range for formula
        /// </summary>
        public (int FromRow, int FromColumn, int ToRow, int ToColumn) CellRange { get; set; }

        /// <inheritdoc />
        public object? ValueObject => null;
    }
}