namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders;
    using Styles;

    /// <summary>
    /// The builder of a single <see cref="Row"/> of a <see cref="Table"/>.
    /// </summary>
    public class RowBuilder : CellsSetBuilder<Row, RowBuilder>, IRowBuilder<Cell>
    {
        /// <inheritdoc />
        public RowBuilder(Row row)
            : base(row)
        {
        }

        /// <summary>
        /// Sets the height of the row.
        /// </summary>
        /// <param name="height">Row height value.</param>
        public IRowBuilder<Cell> SetHeight(double height)
        {
            if (height <= 0)
                throw new ArgumentException("Must be a positive number.", nameof(height));

            ObjectForBuild.OwnHeight = height;
            return this;
        }

        /// <inheritdoc />
        public IRowBuilder<Cell> MergeRow(Action<ICellBuilder<Cell>, ICellBuilder<Cell>>? action = null)
        {
            ((CellBuilder)ObjectForBuild.Cells.First()).MergeNext(ObjectForBuild.Cells.Count() - 1, action);
            return this;
        }

        /// <inheritdoc />
        IRowBuilder<Cell> IRowBuilder<Cell>.SetFormat(CellFormatStyle format)
        {
            SetFormat(format);
            return this;
        }

        /// <inheritdoc />
        IRowBuilder<Cell> IRowBuilder<Cell>.SetFormat(Action<ICellFormatStyleBuilder> action)
        {
            SetFormat(action);
            return this;
        }

        /// <inheritdoc />
        IRowBuilder<Cell> IRowBuilder<Cell>.FromList<TSource>(IList<TSource> source, Action<ICellBuilder<Cell>>? cellsAction)
        {
            FromList(source, cellsAction);
            return this;
        }
    }
}
