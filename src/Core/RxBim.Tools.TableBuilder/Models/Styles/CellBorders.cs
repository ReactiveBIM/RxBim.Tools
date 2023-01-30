namespace RxBim.Tools.TableBuilder
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

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        public CellBorders Clone()
        {
            return (CellBorders)MemberwiseClone();
        }
    }
}