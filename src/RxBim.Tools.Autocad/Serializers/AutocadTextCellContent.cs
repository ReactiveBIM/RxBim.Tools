namespace RxBim.Tools.Autocad.Serializers
{
    /// <summary>
    /// Text cell content for AutoCAD table.
    /// </summary>
    public class AutocadTextCellContent : AutocadTableCellContent<string>
    {
        /// <inheritdoc />
        public AutocadTextCellContent(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Adjust the cell size to fit the text completely.
        /// </summary>
        public bool AdjustCellSize { get; set; }
    }
}