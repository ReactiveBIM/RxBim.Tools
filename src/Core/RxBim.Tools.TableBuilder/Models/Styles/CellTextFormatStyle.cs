namespace RxBim.Tools.TableBuilder
{
    using System.Drawing;

    /// <summary>
    /// Formatting settings for text.
    /// </summary>
    public class CellTextFormatStyle
    {
        /// <summary>
        /// The font family of the text.
        /// </summary>
        public string? FontFamily { get; set; }

        /// <summary>
        /// The text is in bold.
        /// </summary>
        public bool? Bold { get; set; }

        /// <summary>
        /// The text is in italics.
        /// </summary>
        public bool? Italic { get; set; }

        /// <summary>
        /// The font size of the text.
        /// </summary>
        public double? TextSize { get; set; }

        /// <summary>
        /// Use word wrap for long text.
        /// </summary>
        public bool? WrapText { get; set; }

        /// <summary>
        /// The color of the letters for this text.
        /// </summary>
        public Color? TextColor { get; set; }

        /// <summary>
        /// Indicates if text automatically shrinks to fit in the available column width
        /// </summary>
        public bool? ShrinkToFit { get; set; }

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        public CellTextFormatStyle Clone()
        {
            return (CellTextFormatStyle)MemberwiseClone();
        }
    }
}