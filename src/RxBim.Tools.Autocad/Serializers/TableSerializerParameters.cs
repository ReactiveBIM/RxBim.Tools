namespace RxBim.Tools.Autocad.Serializers
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Serialization parameters to AutoCAD table
    /// </summary>
    public class TableSerializerParameters
    {
        /// <summary>
        /// If true, the first row of a table is used as the title of a table.
        /// </summary>
        public bool HasTitle { get; set; }

        /// <summary>
        /// The number of header rows in a table.
        /// </summary>
        public int RowHeadersCount { get; set; } = 1;

        /// <summary>
        /// The default height of a table rows.
        /// </summary>
        public double RowHeightDefault { get; set; } = 8;

        /// <summary>
        /// The style identifier for a table.
        /// </summary>
        public ObjectId TableStyleId { get; set; }

        /// <summary>
        /// The drawing database into which a table is inserted.
        /// </summary>
        public Database? TargetDatabase { get; set; }

        /// <summary>
        /// Bold line weight.
        /// </summary>
        public LineWeight BoldLine { get; set; } = LineWeight.LineWeight050;

        /// <summary>
        /// Thin line weight.
        /// </summary>
        public LineWeight ThinLine { get; set; } = LineWeight.LineWeight018;
    }
}