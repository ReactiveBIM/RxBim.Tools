namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Margins size.
    /// </summary>
    public class CellContentMargins
    {
        /// <summary>
        /// Top margin size.
        /// </summary>
        public double? Top { get; set; }

        /// <summary>
        /// Bottom margin size.
        /// </summary>
        public double? Bottom { get; set; }

        /// <summary>
        /// Left margin size.
        /// </summary>
        public double? Left { get; set; }

        /// <summary>
        /// Right margin size.
        /// </summary>
        public double? Right { get; set; }

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        public CellContentMargins Clone()
        {
            return (CellContentMargins)MemberwiseClone();
        }
    }
}