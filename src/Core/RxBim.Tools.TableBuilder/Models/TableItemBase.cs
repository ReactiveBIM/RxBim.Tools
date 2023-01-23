namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// The base class of the table item.
    /// </summary>
    public abstract class TableItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableItemBase"/> class.
        /// </summary>
        /// <param name="table"><see cref="Table"/></param>
        protected TableItemBase(Table table)
        {
            Table = table;
        }

        /// <summary>
        /// The table that this item belongs to.
        /// </summary>
        public Table Table { get; }

        /// <summary>
        /// Own object format.
        /// </summary>
        public CellFormatStyle Format { get; } = new();

        /// <summary>
        /// Returns a composite format from the object's own format and the object formats that this object belongs to.
        /// </summary>
        public abstract CellFormatStyle GetComposedFormat();
    }
}