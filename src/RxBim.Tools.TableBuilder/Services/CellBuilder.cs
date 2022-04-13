namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Content;
    using Styles;

    /// <summary>
    /// The builder of a single <see cref="Cell"/> of a <see cref="Table"/>.
    /// </summary>
    public class CellBuilder : TableItemBuilderBase<Cell, CellBuilder>
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

        /// <summary>
        /// Returns a new <see cref="RowBuilder"/> for the <see cref="Row"/> that the <see cref="Cell"/> is in.
        /// </summary>
        public RowBuilder ToRow()
        {
            return ObjectForBuild.Row;
        }

        /// <summary>
        /// Returns a new <see cref="ColumnBuilder"/> for the <see cref="Column"/> that the <see cref="Cell"/> is in.
        /// </summary>
        public ColumnBuilder ToColumn()
        {
            return ObjectForBuild.Column;
        }

        /// <summary>
        /// Sets the content of the cell.
        /// </summary>
        /// <param name="data">A cell content object.</param>
        public CellBuilder SetContent(ICellContent data)
        {
            SetToMergedArea(cell => cell.Content = data);
            return this;
        }

        /// <summary>
        /// Sets the width of the cell.
        /// </summary>
        /// <param name="width">The width of the cell.</param>
        public CellBuilder SetWidth(double width)
        {
            ObjectForBuild.Column.OwnWidth = width;
            return this;
        }

        /// <summary>
        /// Sets the height of the cell.
        /// </summary>
        /// <param name="height">The height of the cell.</param>
        public CellBuilder SetHeight(double height)
        {
            ObjectForBuild.Row.OwnHeight = height;
            return this;
        }

        /// <summary>
        /// Set text in the cell.
        /// </summary>
        /// <param name="text">Text value.</param>
        public CellBuilder SetText(string text)
        {
            SetContent(new TextCellContent(text));
            return this;
        }

        /// <summary>
        /// Set value in the cell.
        /// </summary>
        /// <param name="value">Value.</param>
        public CellBuilder SetValue(object value)
        {
            SetContent(new ObjectCellContent(value));
            return this;
        }

        /// <summary>
        /// Returns the <see cref="CellBuilder"/> for the next cell to the right.
        /// </summary>
        /// <param name="step">Offset step to the right.</param>
        public CellBuilder Next(int step = 1)
        {
            return GetNextCellBuilder(Direction.Next, step);
        }

        /// <summary>
        /// Returns the <see cref="CellBuilder"/> for the next cell down.
        /// </summary>
        /// <param name="step">Down offset step.</param>
        public CellBuilder Down(int step = 1)
        {
            return GetNextCellBuilder(Direction.Down, step);
        }

        /// <summary>
        /// Returns the <see cref="CellBuilder"/> for the previous cell to the left.
        /// </summary>
        /// <param name="step">Offset step to the left.</param>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "PublicAPI")]
        public CellBuilder Previous(int step = 1)
        {
            return GetPreviousCellBuilder(ObjectForBuild.Row, step);
        }

        /// <summary>
        /// Returns the <see cref="CellBuilder"/> for the previous cell up.
        /// </summary>
        /// <param name="step">Up offset step.</param>
        public CellBuilder Up(int step = 1)
        {
            return GetPreviousCellBuilder(ObjectForBuild.Column, step);
        }

        /// <summary>
        /// Merges cells horizontally to the right.
        /// </summary>
        /// <param name="count">The number of cells to merge.</param>
        /// <param name="action">Delegate, applied to the cells to be merged.</param>
        /// <returns>Returns the <see cref="CellBuilder"/> of the original cell.</returns>
        public CellBuilder MergeNext(int count = 1, Action<CellBuilder, CellBuilder>? action = null)
        {
            return MergeInternal(count, Direction.Next, action);
        }

        /// <summary>
        /// Merges cells vertically.
        /// </summary>
        /// <param name="count">The number of cells to merge.</param>
        /// <param name="action">Delegate, applied to the cells to be merged.</param>
        /// <returns>Returns the <see cref="CellBuilder"/> of the original cell.</returns>
        public CellBuilder MergeDown(int count = 1, Action<CellBuilder, CellBuilder>? action = null)
        {
            return MergeInternal(count, Direction.Down, action);
        }

        /// <summary>
        /// Merges cells horizontally to the left.
        /// </summary>
        /// <param name="count">The number of cells to merge.</param>
        /// <returns>Returns the <see cref="CellBuilder"/> of the left cell of merged area.</returns>
        public CellBuilder MergeLeft(int count = 1)
        {
            return Previous(count).MergeNext(count).Previous(count);
        }

        /// <summary>
        /// Sets format for the cell.
        /// </summary>
        /// <param name="format">Format value.</param>
        public CellBuilder SetFormat(CellFormatStyle format)
        {
            SetToMergedArea(cell => new CellFormatStyleBuilder(cell.Format).SetFromFormat(format));
            return this;
        }

        /// <summary>
        /// Sets format for the cell.
        /// </summary>
        /// <param name="action">Format building action.</param>
        public CellBuilder SetFormat(Action<CellFormatStyleBuilder> action)
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