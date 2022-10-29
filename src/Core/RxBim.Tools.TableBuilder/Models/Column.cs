namespace RxBim.Tools.TableBuilder
{
    using System.Linq;

    /// <summary>
    /// Table column data.
    /// </summary>
    public class Column : CellsSet
    {
        private double? _ownWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="table">The <see cref="Table"/> that this <see cref="Column"/> belongs to.</param>
        internal Column(Table table)
            : base(table)
        {
        }

        /// <summary>
        /// The width of the column.
        /// </summary>
        public double Width
        {
            get
            {
                if (OwnWidth.HasValue)
                    return OwnWidth.Value;

                var columnsWithValues =
                    Table.Columns.Where(x => x.OwnWidth.HasValue).Select(x => x.OwnWidth!.Value).ToList();
                return (Table.Width - columnsWithValues.Sum()) / (Table.Columns.Count() - columnsWithValues.Count);
            }
        }

        /// <summary>
        /// Own column width.
        /// </summary>
        internal double? OwnWidth
        {
            get => _ownWidth;
            set
            {
                IsAdjustedToContent = false;
                _ownWidth = value;
            }
        }

        /// <summary>
        /// Returns new <see cref="ColumnBuilder"/> for the column.
        /// </summary>
        /// <param name="row"><see cref="Column"/> object.</param>
        public static implicit operator ColumnBuilder(Column row)
        {
            return new ColumnBuilder(row);
        }
    }
}