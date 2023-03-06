namespace RxBim.Tools.TableBuilder
{
    using System.Drawing;

    /// <summary>
    /// Cell formatting settings.
    /// </summary>
    public class CellFormatStyle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellFormatStyle"/> class.
        /// </summary>
        internal CellFormatStyle()
        {
        }

        /// <summary>
        /// Cell text format.
        /// </summary>
        public CellTextFormatStyle TextFormat { get; internal set; } = new();

        /// <summary>
        /// Line types for borders.
        /// </summary>
        public CellBorders Borders { get; internal set; } = new();

        /// <summary>
        /// Margins from borders to content.
        /// </summary>
        public CellContentMargins ContentMargins { get; internal set; } = new();

        /// <summary>
        /// The background color for a cell.
        /// </summary>
        public Color? BackgroundColor { get; internal set; }

        /// <summary>
        /// The horizontal alignment of the contents of a cell.
        /// </summary>
        public CellContentHorizontalAlignment? ContentHorizontalAlignment { get; internal set; }

        /// <summary>
        /// The vertical alignment of the contents of a cell.
        /// </summary>
        public CellContentVerticalAlignment? ContentVerticalAlignment { get; internal set; }
    }
}
