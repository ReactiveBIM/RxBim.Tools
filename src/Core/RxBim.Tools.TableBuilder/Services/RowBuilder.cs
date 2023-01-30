namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The builder of a single <see cref="Row"/> of a <see cref="Table"/>.
    /// </summary>
    public class RowBuilder : CellsSetBuilder<Row, RowBuilder>, IRowBuilder
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
        public IRowBuilder SetHeight(double height)
        {
            if (height <= 0)
                throw new ArgumentException("Must be a positive number.", nameof(height));

            ObjectForBuild.Height = height;
            return this;
        }

        /// <inheritdoc />
        public IRowBuilder MergeRow(Action<ICellBuilder, ICellBuilder>? action = null)
        {
            ((CellBuilder)ObjectForBuild.Cells.First()).MergeNext(ObjectForBuild.Cells.Count() - 1, action);
            return this;
        }

        /// <inheritdoc />
        IRowBuilder IRowBuilder.SetFormat(CellFormatStyle format)
        {
            SetFormat(format);
            return this;
        }

        /// <inheritdoc />
        IRowBuilder IRowBuilder.SetFormat(Action<ICellFormatStyleBuilder> action)
        {
            SetFormat(action);
            return this;
        }

        /// <inheritdoc />
        IRowBuilder IRowBuilder.FromList<TSource>(IList<TSource> source, Action<ICellBuilder>? cellsAction)
        {
            FromList(source, cellsAction);
            return this;
        }

        /// <inheritdoc />
        public Row Build()
        {
            return ObjectForBuild;
        }
    }
}
