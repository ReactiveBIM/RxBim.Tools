namespace RxBim.Tools.TableBuilder
{
    using System.Collections.Generic;

    /// <summary>
    /// Base class for a set of cells.
    /// </summary>
    public abstract class CellsSet : TableItemBase
    {
        private readonly List<Cell> _cells = new();

        /// <inheritdoc />
        protected CellsSet(Table table)
            : base(table)
        {
        }

        /// <summary>
        /// Cells in this set.
        /// </summary>
        public IReadOnlyList<Cell> Cells => _cells;

        /// <summary>
        /// Determines whether the size is adjusted according to the content.
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        public bool IsAdjustedToContent { get; internal set; } = true;

        /// <inheritdoc />
        public override CellFormatStyle GetComposedFormat() => Format.Collect(Table.DefaultFormat);

        /// <summary>
        /// Adds a cell to the set.
        /// </summary>
        /// <param name="cell"><see cref="Cell"/> object.</param>
        /// <returns>Added cell.</returns>
        internal Cell AddCell(Cell cell)
        {
            _cells.Add(cell);
            return cell;
        }
    }
}
