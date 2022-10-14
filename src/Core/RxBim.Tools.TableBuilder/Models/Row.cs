namespace RxBim.Tools.TableBuilder
{
    using System.Linq;

    /// <summary>
    /// Table row data.
    /// </summary>
    public class Row : CellsSet
    {
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
        public double Height
        {
            get
            {
                if (OwnHeight.HasValue)
                    return OwnHeight.Value;

                var rowsWithValues =
                    Table.Rows.Where(x => x.OwnHeight.HasValue).Select(x => x.OwnHeight!.Value).ToList();
                return (Table.Height - rowsWithValues.Sum()) / (Table.Rows.Count() - rowsWithValues.Count);
            }
        }

        /// <summary>
        /// The height of the row.
        /// </summary>
        internal double? OwnHeight { get; set; }

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