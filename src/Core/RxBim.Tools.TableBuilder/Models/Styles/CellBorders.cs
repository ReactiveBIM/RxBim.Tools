namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Line types for cell borders.
    /// </summary>
    public class CellBorders
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellBorders"/> class.
        /// </summary>
        internal CellBorders()
        {
        }
        
        /// <summary>
        /// Top border line type.
        /// </summary>
        public CellBorderType? Top { get; internal set; }

        /// <summary>
        /// Bottom border line type.
        /// </summary>
        public CellBorderType? Bottom { get; internal set; }

        /// <summary>
        /// Left border line type.
        /// </summary>
        public CellBorderType? Left { get; internal set; }

        /// <summary>
        /// Right border line type.
        /// </summary>
        public CellBorderType? Right { get; internal set; }
    }
}