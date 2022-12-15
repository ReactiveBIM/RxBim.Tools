namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Linq;
    using Builders;
    using Content;
    using JetBrains.Annotations;
    using Styles;

    /// <summary>
    /// The builder of a single <see cref="Cell"/> of a <see cref="Table"/>.
    /// </summary>
    [PublicAPI]
    public class CellBuilder : TableItemBuilderBase<Cell, CellBuilder>, ICellBuilder<Cell>
    {
        /// <inheritdoc />
        public CellBuilder(Cell cell)
            : base(cell)
        {
        }

        private enum Direction
        {
            Next,
            Down
        }

        /// <inheritdoc />
        public IRowBuilder<Cell> ToRow()
        {
            return new RowBuilder(ObjectForBuild.Row);
        }

        /// <inheritdoc />
        public IColumnBuilder<Cell> ToColumn()
        {
            return new ColumnBuilder(ObjectForBuild.Column);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> SetContent(ICellContent data)
        {
            SetToMergedArea(cell => cell.Content = data);
            return this;
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> SetWidth(double width)
        {
            ObjectForBuild.Column.Width = width;
            return this;
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> SetHeight(double height)
        {
            ObjectForBuild.Row.Height = height;
            return this;
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> SetText(string text)
        {
            SetContent(new TextCellContent(text));
            return this;
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> SetValue(object value)
        {
            SetContent(new ObjectCellContent(value));
            return this;
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> Next(int step = 1)
        {
            return GetNextCellBuilder(Direction.Next, step);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> Down(int step = 1)
        {
            return GetNextCellBuilder(Direction.Down, step);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> Previous(int step = 1)
        {
            return GetPreviousCellBuilder(ObjectForBuild.Row, step);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> Up(int step = 1)
        {
            return GetPreviousCellBuilder(ObjectForBuild.Column, step);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> MergeNext(int count = 1, Action<ICellBuilder<Cell>, ICellBuilder<Cell>>? action = null)
        {
            return MergeInternal(count, Direction.Next, action);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> MergeDown(int count = 1, Action<ICellBuilder<Cell>, ICellBuilder<Cell>>? action = null)
        {
            return MergeInternal(count, Direction.Down, action);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> MergeLeft(int count = 1)
        {
            return Previous(count).MergeNext(count).Previous(count);
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> SetFormat(CellFormatStyle format)
        {
            SetToMergedArea(cell => new CellFormatStyleBuilder(cell.Format).SetFromFormat(format));
            return this;
        }

        /// <inheritdoc />
        public ICellBuilder<Cell> SetFormat(Action<ICellFormatStyleBuilder> action)
        {
            SetToMergedArea(cell => action(new CellFormatStyleBuilder(cell.Format)));
            return this;
        }

        private CellBuilder GetNextCellBuilder(Direction direction, int step)
        {
            var cellsSet = direction == Direction.Next ? (CellsSet)ObjectForBuild.Row : ObjectForBuild.Column;
            var cellIndex = cellsSet.Cells.IndexOf(ObjectForBuild);

            if (cellsSet.Cells.Count() <= cellIndex + step)
                throw new ArgumentOutOfRangeException(nameof(step));

            return new CellBuilder(cellsSet.Cells[cellIndex + step]);
        }

        private CellBuilder GetPreviousCellBuilder(CellsSet cellsSet, int step)
        {
            var cellIndex = cellsSet.Cells.IndexOf(ObjectForBuild);

            if (cellIndex - step < 0)
                throw new ArgumentOutOfRangeException(nameof(step));

            return new CellBuilder(cellsSet.Cells[cellIndex - step]);
        }

        private CellBuilder MergeInternal(int count, Direction direction, Action<CellBuilder, CellBuilder>? action)
        {
            var area = CreateMergeArea(count, direction);

            for (var row = area.TopRow; row <= area.BottomRow; row++)
            {
                for (var column = area.LeftColumn; column <= area.RightColumn; column++)
                {
                    var cell = ObjectForBuild.Table[row, column];
                    cell.MergeArea = area;
                    if (cell.Equals(ObjectForBuild))
                        continue;

                    var cellBuilder = (CellBuilder)cell;
                    action?.Invoke(this, cellBuilder);
                    cellBuilder.SetContent(ObjectForBuild.Content);
                }
            }

            return GetNextCellBuilder(direction, count);
        }

        private CellRange CreateMergeArea(int count, Direction direction)
        {
            var existsMergeArea = ObjectForBuild.MergeArea;

            int topRow, leftColumn, bottomRow, rightColumn;

            if (existsMergeArea != null)
            {
                topRow = existsMergeArea.Value.TopRow;
                leftColumn = existsMergeArea.Value.LeftColumn;
                bottomRow = existsMergeArea.Value.BottomRow;
                rightColumn = existsMergeArea.Value.RightColumn;
            }
            else
            {
                topRow = ObjectForBuild.GetRowIndex();
                leftColumn = ObjectForBuild.GetColumnIndex();
                bottomRow = topRow;
                rightColumn = leftColumn;
            }

            switch (direction)
            {
                case Direction.Down:
                    bottomRow += count;
                    break;
                case Direction.Next:
                    rightColumn += count;
                    break;
            }

            var area = ObjectForBuild.Table.AddMergeArea(topRow, leftColumn, bottomRow, rightColumn);
            return area;
        }

        private void SetToMergedArea(Action<Cell> action)
        {
            var area = ObjectForBuild.MergeArea;
            if (area != null)
            {
                for (var column = area.Value.LeftColumn; column <= area.Value.RightColumn; column++)
                {
                    for (var row = area.Value.TopRow; row <= area.Value.BottomRow; row++)
                        action?.Invoke(ObjectForBuild.Table[row, column]);
                }
            }
            else
            {
                action.Invoke(ObjectForBuild);
            }
        }
    }
}
