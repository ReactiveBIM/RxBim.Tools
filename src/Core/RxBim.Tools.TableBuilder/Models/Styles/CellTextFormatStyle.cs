namespace RxBim.Tools.TableBuilder
{
    using System.Drawing;

    /// <summary>
    /// Formatting settings for text.
    /// </summary>
    public class CellTextFormatStyle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellTextFormatStyle"/> class.
        /// </summary>
        internal CellTextFormatStyle()
        {
        }
        
        /// <summary>
        /// The font family of the text.
        /// </summary>
        public string? FontFamily { get; internal set; }

        /// <summary>
        /// The text is in bold.
        /// </summary>
        public bool? Bold { get; internal set; }

        /// <summary>
        /// The text is in italics.
        /// </summary>
        public bool? Italic { get; internal set; }

        /// <summary>
        /// The font size of the text.
        /// </summary>
        public double? TextSize { get; internal set; }

        /// <summary>
        /// Use word wrap for long text.
        /// </summary>
        public bool? WrapText { get; internal set; }

        /// <summary>
        /// The color of the letters for this text.
        /// </summary>
        public Color? TextColor { get; internal set; }

        /// <summary>
        /// Indicates if text automatically shrinks to fit in the available column width
        /// </summary>
        public bool? ShrinkToFit { get; internal set; }
    }
}