namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The builder of a <see cref="CellsSet"/> of a <see cref="Table"/>.
    /// </summary>
    /// <typeparam name="TSet">Type of <see cref="CellsSet"/> implementation.</typeparam>
    /// <typeparam name="TBuilder">Type of <see cref="CellsSetEditor{TSet,TBuilder}"/> implementation.</typeparam>
    internal abstract class CellsSetEditor<TSet, TBuilder> : TableItemEditorBase<TSet, TBuilder>
        where TSet : CellsSet
        where TBuilder : CellsSetEditor<TSet, TBuilder>
    {
        /// <inheritdoc />
        protected CellsSetEditor(TSet set)
            : base(set)
        {
        }

        /// <summary>
        /// Returns collection of <see cref="CellEditor"/> for cells.
        /// </summary>
        public IEnumerable<ICellEditor> Cells
            => ObjectForBuild.Cells.Select(cell => new CellEditor(cell));
        
        /// <summary>
        /// Fills cells with text values from list items.
        /// </summary>
        /// <param name="source">List of items.</param>
        /// <param name="cellsAction">Delegate. Applies to all filled cells.</param>
        /// <typeparam name="TSource">The type of the list item.</typeparam>
        public TBuilder FromList<TSource>(IList<TSource> source, Action<ICellEditor>? cellsAction = null)
        {
            if (!source.Any())
                return (TBuilder)this;

            if (source.Count > ObjectForBuild.Cells.Count())
            {
                throw new ArgumentException(
                    "The number of items in the list should not be more than the number of cells in this set!");
            }

            for (var i = 0; i < source.Count; i++)
            {
                var cell = new CellEditor(ObjectForBuild.Cells[i]);
                cell.SetContent(new TextCellContent(source[i]?.ToString() ?? string.Empty));
                cellsAction?.Invoke(cell);
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// Sets format for the cells set.
        /// </summary>
        /// <param name="format">Format value.</param>
        public TBuilder SetFormat(CellFormatStyle format)
        {
            new CellFormatStyleBuilder(ObjectForBuild.Format).SetFromFormat(format);
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets format for the cells set.
        /// </summary>
        /// <param name="adjustToContent">Adjust to content value.</param>
        public TBuilder SetAdjustToContent(bool adjustToContent)
        {
            ObjectForBuild.IsAdjustedToContent = adjustToContent;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets format for the cells set.
        /// </summary>
        /// <param name="action">Format building action.</param>
        public TBuilder SetFormat(Action<ICellFormatStyleBuilder> action)
        {
            action(new CellFormatStyleBuilder(ObjectForBuild.Format));
            return (TBuilder)this;
        }
    }
}
