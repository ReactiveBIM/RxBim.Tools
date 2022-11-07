namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Table row data.
    /// </summary>
    public class Row : CellsSet
    {
        private double? _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        /// <param name="table">The <see cref="Table"/> that this <see cref="Row"/> belongs to.</param>
        internal Row(Table table)
            : base(table)
        {
        }

        /// <summary>
        /// The height of the row.
        /// </summary>
        public double? Height
        {
            get => _height;
            set
            {
                IsAdjustedToContent = false;
                _height = value;
            }
        }

        /// <summary>
        /// Returns new <see cref="RowBuilder"/> for the row.
        /// </summary>
        /// <param name="row"><see cref="Row"/> object.</param>
        public static implicit operator RowBuilder(Row row)
        {
            return new RowBuilder(row);
        }
    }
}