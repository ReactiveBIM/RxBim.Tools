namespace RxBim.Tools.TableBuilder.Models.Styles
{
    /// <summary>
    /// Line types for cell borders.
    /// </summary>
    public class CellBorders
    {
        /// <summary>
        /// Top border line type.
        /// </summary>
        public CellBorderType? Top { get; set; }

        /// <summary>
        /// Bottom border line type.
        /// </summary>
        public CellBorderType? Bottom { get; set; }

        /// <summary>
        /// Left border line type.
        /// </summary>
        public CellBorderType? Left { get; set; }

        /// <summary>
        /// Right border line type.
        /// </summary>
        public CellBorderType? Right { get; set; }
    }
}