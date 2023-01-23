namespace RxBim.Tools.TableBuilder
{
    using System;

    /// <summary>
    /// The builder of a single <see cref="Column"/> of a <see cref="Table"/>.
    /// </summary>
    public class ColumnBuilder : CellsSetBuilder<Column, ColumnBuilder>, IColumnBuilder<Cell>
    {
        /// <inheritdoc />
        public ColumnBuilder(Column column)
            : base(column)
        {
        }

        /// <summary>
        /// Sets the width of the column.
        /// </summary>
        /// <param name="width">Column width.</param>
        public IColumnBuilder<Cell> SetWidth(double width)
        {
            if (width <= 0)
                throw new ArgumentException("Must be a positive number.", nameof(width));

            ObjectForBuild.Width = width;
            return this;
        }

        /// <inheritdoc />
        IColumnBuilder<Cell> IColumnBuilder<Cell>.SetFormat(CellFormatStyle format)
        {
            SetFormat(format);
            return this;
        }

        /// <inheritdoc />
        IColumnBuilder<Cell> IColumnBuilder<Cell>.SetFormat(Action<ICellFormatStyleBuilder> action)
        {
            SetFormat(action);
            return this;
        }
    }
}
