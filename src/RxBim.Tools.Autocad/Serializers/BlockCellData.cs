namespace RxBim.Tools.Autocad.Serializers
{
    using Autodesk.AutoCAD.DatabaseServices;
    using TableBuilder.Models.Contents;

    /// <summary>
    /// Data to insert a block into a cell.
    /// </summary>
    public class BlockCellData : CellContent<ObjectId>
    {
        /// <inheritdoc />
        public BlockCellData(ObjectId value)
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
        /// Rotation angle in radians.
        /// </summary>
        public double Rotation { get; set; }
    }
}