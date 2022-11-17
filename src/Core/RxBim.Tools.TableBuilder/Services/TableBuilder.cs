namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders;
    using Content;
    using Styles;

    /// <summary>
    /// The builder of a <see cref="Table"/>.
    /// </summary>
    public class TableBuilder : ITableBuilder
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
        private Table Table { get; }

        /// <inheritdoc />
        public ICellBuilder this[int row, int column] => new CellBuilder(Table[row, column]);

        /// <summary>
        /// Returns the built <see cref="Table"/>.
        /// </summary>
        /// <param name="builder"><see cref="TableBuilder"/> object.</param>
        public static implicit operator Table(TableBuilder builder)
        {
            return builder.Build();
        }

        /// <inheritdoc />
        public IEnumerable<IRowBuilder> ToRows()
        {
            return Table.Rows.Select(row => new RowBuilder(row));
        }

        /// <inheritdoc />
        public IEnumerable<IColumnBuilder> GetColumns()
        {
            return Table.Columns.Select(column => new ColumnBuilder(column));
        }

        /// <inheritdoc />
        public ITableBuilder SetFormat(CellFormatStyle formatStyle)
        {
            Table.DefaultFormat = formatStyle;
            return this;
        }

        /// <inheritdoc />
        public ITableBuilder SetFormat(Action<ICellFormatStyleBuilder> action)
        {
            action(new CellFormatStyleBuilder(Table.DefaultFormat));
            return this;
        }

        /// <inheritdoc />
        public ITableBuilder SetCellsFormat(
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

        /// <inheritdoc />
        public ITableBuilder SetTableStateStandardFormat(int headerRowsCount)
        {
            var boldFormat = new CellFormatStyleBuilder()
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                .SetBorders(builder => builder.SetAllBorders(CellBorderType.Bold))
                .Build();

            var rowFormat = new CellFormatStyleBuilder()
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                .SetBorders(builder =>
                    builder.SetBorders(CellBorderType.Thin, CellBorderType.Thin, CellBorderType.Bold, CellBorderType.Bold))
                .Build();

            var lastRowFormat = new CellFormatStyleBuilder()
                .SetContentVerticalAlignment(CellContentVerticalAlignment.Middle)
                .SetBorders(builder =>
                    builder.SetBorders(CellBorderType.Thin, CellBorderType.Bold, CellBorderType.Bold, CellBorderType.Bold))
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

        /// <inheritdoc />
        public ITableBuilder SetWidth(double width)
        {
            if (width <= 0)
                throw new ArgumentException("Must be a positive number.", nameof(width));
            Table.OwnWidth = width;
            return this;
        }

        /// <inheritdoc />
        public ITableBuilder SetHeight(double height)
        {
            if (height <= 0)
                throw new ArgumentException("Must be a positive number.", nameof(height));
            Table.OwnHeight = height;
            return this;
        }

        /// <inheritdoc />
        public ITableBuilder AddRow(Action<IRowBuilder>? action = null, int count = 1)
        {
            for (; count > 0; count--)
            {
                var newRow = Table.AddRow();
                action?.Invoke(new RowBuilder(newRow));
            }

            return this;
        }

        /// <inheritdoc />
        public ITableBuilder AddRowsFromList<TSource>(
            IEnumerable<TSource> source,
            int rowIndex,
            int columnIndex,
            params Func<TSource, object>[] propertySelectors)
        {
            var list = source.ToList();

            if (!list.Any())
                return this;

            var selectorList = GetSelectorList(propertySelectors);

            var matrix = new ICellContent[list.Count, selectorList.Count];

            for (var c = 0; c < selectorList.Count; c++)
            {
                var prop = selectorList[c];
                for (var r = 0; r < list.Count; r++)
                {
                    var value = prop.Invoke(list[r]);
                    matrix[r, c] = prop is ICellContent propContent
                        ? propContent
                        : new TextCellContent(value.ToString());
                }
            }

            FromMatrix(rowIndex, columnIndex, matrix);

            return this;
        }

        /// <inheritdoc />
        public ITableBuilder AddColumn(Action<IColumnBuilder>? action = null, int count = 1)
        {
            for (; count > 0; count--)
            {
                var newColumn = Table.AddColumn();
                action?.Invoke(new ColumnBuilder(newColumn));
            }

            return this;
        }

        /// <inheritdoc />
        public ITableBuilder AddColumnsFromList<TSource>(
            IEnumerable<TSource> source,
            int rowIndex,
            int columnIndex,
            params Func<TSource, object>[] propertySelectors)
        {
            var list = source.ToList();
            if (!list.Any())
                return this;

            var selectorList = GetSelectorList(propertySelectors);

            var matrix = new ICellContent[selectorList.Count, list.Count];

            for (var r = 0; r < selectorList.Count; r++)
            {
                var prop = selectorList[r];
                for (var c = 0; c < list.Count; c++)
                {
                    var value = prop.Invoke(list[c]);
                    matrix[r, c] = prop is ICellContent propContent
                        ? propContent
                        : new TextCellContent(value.ToString());
                }
            }

            FromMatrix(rowIndex, columnIndex, matrix);

            return this;
        }

        /// <inheritdoc />
        public Table Build()
        {
            return Table;
        }

        private void FromMatrix(
            int row,
            int column,
            ICellContent[,] matrix)
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

        private List<Func<TSource, object>> GetSelectorList<TSource>(IEnumerable<Func<TSource, object>> propertySelectors)
        {
            var selectorList = propertySelectors.ToList();
            if (!selectorList.Any())
                selectorList.Add(x => x?.ToString() ?? string.Empty);
            return selectorList;
        }
    }
}
