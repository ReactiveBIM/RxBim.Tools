namespace RxBim.Tools.TableBuilder
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Data to insert a block into a cell.
    /// </summary>
    public class BlockCellContent : AutocadTableCellContent<ObjectId>
    {
        /// <inheritdoc />
        public BlockCellContent(ObjectId value)
            : base(value)
        {
        }

        /// <summary>
        /// Use autoscaling.
        /// </summary>
        public bool AutoScale { get; set; }

        /// <summary>
        /// Scale value.
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// The text to be added to the cell with the block.
        /// </summary>
        public string? Text { get; set; }
    }
}