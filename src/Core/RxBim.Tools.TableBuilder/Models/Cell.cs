namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Table cell data.
    /// </summary>
    public class Cell : TableItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="table">The <see cref="Table"/> that this <see cref="Cell"/> belongs to.</param>
        /// <param name="row">The row that contains this cell.</param>
        /// <param name="column">The column that contains this cell.</param>
        internal Cell(Table table, Row row, Column column)
            : base(table)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// The row that contains this cell.
        /// </summary>
        public Row Row { get; }

        /// <summary>
        /// The column that contains this cell.
        /// </summary>
        public Column Column { get; }

        /// <summary>
        /// Returns true, if this cell is used in merging cells. Otherwise returns false.
        /// </summary>
        public bool Merged => MergeArea != null;

        /// <summary>
        /// Cell content.
        /// </summary>
        public ICellContent Content { get; internal set; } = TextCellContent.Empty;

        /// <summary>
        /// Merge area in which this cell is used.
        /// </summary>
        /// <remarks>If is null, this cell is not used in merging cells.</remarks>
        public CellRange? MergeArea { get; internal set; }

        /// <inheritdoc />
        public override CellFormatStyle GetComposedFormat() =>
            Format.Collect(Row.Format.Collect(Column.GetComposedFormat()));

        /// <inheritdoc />
        public override string ToString()
        {
            return Content.ToString();
        }
    }
}