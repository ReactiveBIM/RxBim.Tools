namespace RxBim.Tools.TableBuilder.Models.Styles
{
    using System.Drawing;

    /// <summary>
    /// Cell formatting settings.
    /// </summary>
    public class CellFormatStyle
    {
        /// <summary>
        /// Cell text format.
        /// </summary>
        public CellTextFormatStyle TextFormat { get; } = new ();

        /// <summary>
        /// Line types for borders.
        /// </summary>
        public CellBorders Borders { get; } = new ();

        /// <summary>
        /// Margins from borders to content.
        /// </summary>
        public CellContentMargins? ContentMargins { get; set; }

        /// <summary>
        /// The background color for a cell.
        /// </summary>
        public Color? BackgroundColor { get; set; }

        /// <summary>
        /// The horizontal alignment of the contents of a cell.
        /// </summary>
        public CellContentHorizontalAlignment? ContentHorizontalAlignment { get; set; }

        /// <summary>
        /// The vertical alignment of the contents of a cell.
        /// </summary>
        public CellContentVerticalAlignment? ContentVerticalAlignment { get; set; }

        /// <summary>
        /// Cell content orientation.
        /// </summary>
        public CellContentOrientation? ContentOrientation { get; set; }
    }
}