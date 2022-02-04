namespace RxBim.Tools.Autocad.Serializers
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
    }
}