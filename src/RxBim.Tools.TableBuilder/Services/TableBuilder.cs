namespace RxBim.Tools.TableBuilder.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Models.Contents;
    using Models.Styles;

    /// <summary>
    /// The builder of a <see cref="Table"/>.
    /// </summary>
    public class TableBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableBuilder"/> class.
        /// </summary>
        /// <param name="table">The <see cref="Table"/> to be built.</param>
        public TableBuilder(Table? table = null)
        {
            Table = table ?? new Table();
        }

        /// <summary>
        /// The table to be built.
        /// </summary>
        internal Table Table { get; }

        /// <summary>
        /// Returns the builder of a table cell by row and column index.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        public CellBuilder this[int row, int column] => new (Table[row, column]);

        /// <summary>
        /// Returns the built <see cref="Table"/>.
        /// </summary>
        /// <param name="builder"><see cref="TableBuilder"/> object.</param>
        public static implicit operator Table(TableBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Returns collection of <see cref="RowBuilder"/> for table rows.
        /// </summary>
        public IEnumerable<RowBuilder> ToRows()
        {
            return Table.Rows.Select(row => (RowBuilder)row);
        }

        /// <summary>
        /// Returns collection of <see cref="ColumnBuilder"/> for table rows.
        /// </summary>
        public IEnumerable<ColumnBuilder> GetColumns()
        {
            return Table.Columns.Select(column => (ColumnBuilder)column);
        }

        /// <summary>
        /// Sets the default format for all cells.
        /// </summary>
        /// <param name="formatStyle">Format value.</param>
        /// <returns>
        /// Used if the cell doesn't have its own format and the default format is not set with a row or column.
        /// </returns>
        public TableBuilder SetFormat(CellFormatStyle formatStyle)
        {
            Table.DefaultFormat = formatStyle;
            return this;
        }

        /// <summary>
        /// Sets format for the table.
        /// </summary>
        /// <param name="action">Format building action.</param>
        public TableBuilder SetFormat(Action<CellFormatStyleBuilder> action)
        {
            action(new CellFormatStyleBuilder(Table.DefaultFormat));
            return this;
        }

        /// <summary>
        /// Sets the format for a range of cells.
        /// </summary>
        /// <param name="formatStyle">Format value.</param>
        /// <param name="startRow">The starting row of the range of cells.</param>
        /// <param name="startColumn">The starting column of the range of cells.</param>
        /// <param name="rangeWidth">The width of the range of cells (number of columns).</param>
        /// <param name="rangeHeight">The height of the range of cells (number of rows).</param>
        public TableBuilder SetCellsFormat(
            CellFormatStyle formatStyle,
            int startRow,
            int startColumn,
            int rangeWidth,
            int rangeHeight)
        {
            foreach (var column in Table.Columns.Skip(startColumn).Take(rangeWidth))
            {
                foreach (var cell in column.Cells.Skip(startRow).Take(rangeHeight))
                {
                    new CellFormatStyleBuilder(cell.Format).SetFromFormat(formatStyle);
                }
            }

            return this;
        }

        /// <summary>
        /// Sets the standard format for the table.
        /// </summary>
        /// <param name="headerRowsCount">Table header rows count.</param>
        /// <remarks>
        /// All text centered, all header lines bold.
        /// All horizontal borders between table rows are of standard thickness.
        /// </remarks>
        public TableBuilder SetTableStateStandardFormat(int headerRowsCount)
        {
            var boldFormat = new CellFormatStyleBuilder()
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                .SetAllBorders(CellBorderType.Bold)
                .Build();

            var rowFormat = new CellFormatStyleBuilder()
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                .SetBorders(CellBorderType.Thin, CellBorderType.Thin, CellBorderType.Bold, CellBorderType.Bold)
                .Build();

            var lastRowFormat = new CellFormatStyleBuilder()
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                .SetBorders(CellBorderType.Thin, CellBorderType.Bold, CellBorderType.Bold, CellBorderType.Bold)
                .Build();

            SetFormat(boldFormat);

            if (Table.Rows.Count() > headerRowsCount + 1)
            {
                SetCellsFormat(rowFormat,
                    headerRowsCount + 1,
                    0,
                    Table.Columns.Count(),
                    Table.Rows.Count() - headerRowsCount);
                foreach (var cell in Table.Rows.Last().Cells)
                    ((CellBuilder)cell).SetFormat(lastRowFormat);
            }

            return this;
        }

        /// <summary>
        /// Sets the width of the table.
        /// </summary>
        /// <param name="width">Width value/</param>
        public TableBuilder SetWidth(double width)
        {
            if (width <= 0)
                throw new ArgumentException("Must be a positive number.", nameof(width));
            Table.OwnWidth = width;
            return this;
        }

        /// <summary>
        /// Sets the height of the table.
        /// </summary>
        /// <param name="height">Height value/</param>
        public TableBuilder SetHeight(double height)
        {
            if (height <= 0)
                throw new ArgumentException("Must be a positive number.", nameof(height));
            Table.OwnHeight = height;
            return this;
        }

        /// <summary>
        /// Creates and adds rows to the table.
        /// </summary>
        /// <param name="action">The action to be taken on new rows.</param>
        /// <param name="count">The number of rows to add.</param>
        public TableBuilder AddRow(Action<RowBuilder>? action = null, int count = 1)
        {
            for (; count > 0; count--)
            {
                var newRow = Table.AddRow();
                action?.Invoke(newRow);
            }

            return this;
        }

        /// <summary>
        /// Fills table rows with values from a list.
        /// </summary>
        /// <param name="source">
        /// A list is a source of values. The properties retrieved by the <paramref name="propertySelectors"/>
        /// parameter of each individual item in this list will end up on a separate row.
        /// </param>
        /// <param name="rowIndex">The index of the initial row to fill.</param>
        /// <param name="columnIndex">The index of the initial column to fill.</param>
        /// <param name="propertySelectors">
        /// Functions for getting values from a source list.
        /// Each separate function retrieves the value for the cells in a particular column.
        /// </param>
        /// <typeparam name="TSource">The type of object in the source list.</typeparam>
        public TableBuilder AddRowsFromList<TSource>(
            IEnumerable<TSource> source,
            int rowIndex,
            int columnIndex,
            params Func<TSource, object>[] propertySelectors)
        {
            var list = source.ToList();

            if (!list.Any())
                return this;

            var selectorList = GetSelectorList(propertySelectors);

            var matrix = new object[list.Count, selectorList.Count];

            for (var c = 0; c < selectorList.Count; c++)
            {
                var prop = selectorList[c];
                for (var r = 0; r < list.Count; r++)
                {
                    var value = prop.Invoke(list[r]);
                    matrix[r, c] = new TextCellContent(value.ToString());
                }
            }

            FromMatrix(rowIndex, columnIndex, matrix);

            return this;
        }

        /// <summary>
        /// Creates and adds columns to the table.
        /// </summary>
        /// <param name="action">The action to be taken on new columns.</param>
        /// <param name="count">The number of columns to add.</param>
        public TableBuilder AddColumn(Action<ColumnBuilder>? action = null, int count = 1)
        {
            for (; count > 0; count--)
            {
                var newColumn = Table.AddColumn();
                action?.Invoke(newColumn);
            }

            return this;
        }

        /// <summary>
        /// Fills table columns with values from a list.
        /// </summary>
        /// <param name="source">
        /// A list is a source of values. The properties retrieved by the <paramref name="propertySelectors"/>
        /// parameter of each individual item in this list will end up on a separate column.
        /// </param>
        /// <param name="rowIndex">The index of the initial row to fill.</param>
        /// <param name="columnIndex">The index of the initial column to fill.</param>
        /// <param name="propertySelectors">
        /// Functions for getting values from a source list.
        /// Each separate function retrieves the value for the cells in a particular row.
        /// </param>
        /// <typeparam name="TSource">The type of object in the source list.</typeparam>
        public TableBuilder AddColumnsFromList<TSource>(
            IEnumerable<TSource> source,
            int rowIndex,
            int columnIndex,
            params Func<TSource, object>[] propertySelectors)
        {
            var list = source.ToList();
            if (!list.Any())
                return this;

            var selectorList = GetSelectorList(propertySelectors);

            var matrix = new object[selectorList.Count, list.Count];

            for (var r = 0; r < selectorList.Count; r++)
            {
                var prop = selectorList[r];
                for (var c = 0; c < list.Count; c++)
                {
                    var value = prop.Invoke(list[c]);
                    matrix[r, c] = new TextCellContent(value.ToString());
                }
            }

            FromMatrix(rowIndex, columnIndex, matrix);

            return this;
        }

        /// <summary>
        /// Returns the built <see cref="Table"/>.
        /// </summary>
        public Table Build()
        {
            return Table;
        }

        private void FromMatrix(
            int row,
            int column,
            object[,] matrix)
        {
            var diffRows = row + matrix.GetLength(0) - Table.Rows.Count();
            var diffCols = column + matrix.GetLength(1) - Table.Columns.Count();

            AddRow(count: diffRows);
            AddColumn(count: diffCols);

            for (var r = 0; r < matrix.GetLength(0); r++)
            {
                for (var c = 0; c < matrix.GetLength(1); c++)
                    this[row + r, column + c].SetContent(matrix[r, c]);
            }
        }

        private List<Func<TSource, object>> GetSelectorList<TSource>(
            IEnumerable<Func<TSource, object>> propertySelectors)
        {
            var selectorList = propertySelectors.ToList();
            if (!selectorList.Any())
                selectorList.Add(x => x?.ToString() ?? string.Empty);
            return selectorList;
        }
    }
}