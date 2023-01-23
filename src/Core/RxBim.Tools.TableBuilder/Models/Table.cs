namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Table data.
    /// </summary>
    public class Table
    {
        private readonly List<Column> _columns = new();
        private readonly List<Row> _rows = new();
        private readonly List<CellRange> _mergeAreas = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        internal Table()
        {
        }

        /// <summary>
        /// Columns of this table.
        /// </summary>
        public IReadOnlyList<Column> Columns => _columns;

        /// <summary>
        /// Rows of this table.
        /// </summary>
        public IReadOnlyList<Row> Rows => _rows;

        /// <summary>
        /// Merge areas of table cells.
        /// </summary>
        public IReadOnlyList<CellRange> MergeAreas => _mergeAreas;

        /// <summary>
        /// The width of the table.
        /// </summary>
        public double Width => Math.Max(_columns.Sum(x => x.Width ?? 0), OwnWidth);

        /// <summary>
        /// The height of the table.
        /// </summary>
        public double Height => Math.Max(_rows.Sum(x => x.Height ?? 0), OwnHeight);

        /// <summary>
        /// The default format for cells.
        /// </summary>
        public CellFormatStyle DefaultFormat { get; set; } = new();

        /// <summary>
        /// Own width.
        /// </summary>
        internal double OwnWidth { get; set; }

        /// <summary>
        /// Own height.
        /// </summary>
        internal double OwnHeight { get; set; }

        /// <summary>
        /// Returns a cell from the table.
        /// </summary>
        /// <param name="row">Cell row index.</param>
        /// <param name="column">Cell column index.</param>
        public Cell this[int row, int column]
        {
            get
            {
                if (row < 0)
                    throw new ArgumentException("Must be a positive number.", nameof(row));

                if (column < 0)
                    throw new ArgumentException("Must be a positive number.", nameof(column));

                if (row >= _rows.Count)
                    throw new IndexOutOfRangeException($"Row {row} doesn't exist!");

                if (column >= _columns.Count)
                    throw new IndexOutOfRangeException($"Column {column} doesn't exist!");

                return _rows[row].Cells[column];
            }
        }

        /// <summary>
        /// Adds a new row to the table.
        /// </summary>
        /// <returns>Added row.</returns>
        internal Row AddRow()
        {
            var newRow = new Row(this);

            foreach (var column in _columns)
            {
                var cell = new Cell(this, newRow, column);
                newRow.AddCell(cell);
                column.AddCell(cell);
            }

            _rows.Add(newRow);

            return newRow;
        }

        /// <summary>
        /// Adds a new column to the table.
        /// </summary>
        /// <returns>Added column.</returns>
        internal Column AddColumn()
        {
            var newColumn = new Column(this);

            foreach (var row in _rows)
            {
                var cell = new Cell(this, row, newColumn);
                newColumn.AddCell(cell);
                row.AddCell(cell);
            }

            _columns.Add(newColumn);

            return newColumn;
        }

        /// <summary>
        /// Adds a new merge area for table cells.
        /// </summary>
        /// <param name="topRow"><see cref="CellRange.TopRow"/> for new area.</param>
        /// <param name="leftColumn"><see cref="CellRange.LeftColumn"/> for new area.</param>
        /// <param name="bottomRow"><see cref="CellRange.BottomRow"/> for new area.</param>
        /// <param name="rightColumn"><see cref="CellRange.RightColumn"/> for new area.</param>
        /// <exception cref="InvalidOperationException">
        /// If new merge area intersects an existing area in the table.
        /// </exception>
        internal CellRange AddMergeArea(int topRow, int leftColumn, int bottomRow, int rightColumn)
        {
            var lastRow = _rows.Count - 1;
            var lastColumn = _columns.Count - 1;

            if (topRow > lastRow || bottomRow > lastRow || leftColumn > lastColumn || rightColumn > lastColumn)
            {
                throw new ArgumentException(
                    "Unable to create a merge area because the range is out of the size of the table. " +
                    $"Table size: rows={_rows.Count}, columns={_columns.Count}. " +
                    $"Range: {nameof(topRow)}={topRow}, {nameof(leftColumn)}={leftColumn}," +
                    $" {nameof(bottomRow)}={bottomRow}, {nameof(rightColumn)}={rightColumn}");
            }

            var range = new CellRange(topRow, bottomRow, leftColumn, rightColumn);
            if (!range.IsValid)
            {
                throw new ArgumentException(
                    "Unable to create merge region due to invalid range. " +
                    $"Range: {nameof(topRow)}={topRow}, {nameof(leftColumn)}={leftColumn}," +
                    $" {nameof(bottomRow)}={bottomRow}, {nameof(rightColumn)}={rightColumn}");
            }

            if (_mergeAreas.Any(x => !x.IsInsideFor(range) && x.IsIntersectWith(range)))
                throw new InvalidOperationException("The merge range intersects an existing range in the table.");
            _mergeAreas.RemoveAll(x => x.IsInsideFor(range));
            _mergeAreas.Add(range);
            return range;
        }
    }
}