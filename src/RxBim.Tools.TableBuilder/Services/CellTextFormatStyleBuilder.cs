namespace RxBim.Tools.TableBuilder.Services
{
    using System.Drawing;
    using Models.Styles;

    /// <summary>
    /// Builder for <see cref="CellTextFormatStyle"/>
    /// </summary>
    public class CellTextFormatStyleBuilder
    {
        private readonly CellTextFormatStyle _textFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellTextFormatStyleBuilder"/> class.
        /// </summary>
        /// <param name="textFormat">Format for build.</param>
        public CellTextFormatStyleBuilder(CellTextFormatStyle? textFormat = null)
        {
            _textFormat = textFormat ?? new CellTextFormatStyle();
        }

        /// <summary>
        /// Sets <see cref="CellTextFormatStyle.FontFamily"/> property.
        /// </summary>
        /// <param name="fontFamily">Property value.</param>
        public CellTextFormatStyleBuilder SetFontFamily(string? fontFamily)
        {
            _textFormat.FontFamily = fontFamily;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellTextFormatStyle.Bold"/> property.
        /// </summary>
        /// <param name="bold">Property value.</param>
        public CellTextFormatStyleBuilder SetBold(bool? bold)
        {
            _textFormat.Bold = bold;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellTextFormatStyle.Italic"/> property.
        /// </summary>
        /// <param name="italic">Property value.</param>
        public CellTextFormatStyleBuilder SetItalic(bool? italic)
        {
            _textFormat.Italic = italic;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellTextFormatStyle.TextSize"/> property.
        /// </summary>
        /// <param name="textSize">Property value.</param>
        public CellTextFormatStyleBuilder SetTextSize(double? textSize)
        {
            _textFormat.TextSize = textSize;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellTextFormatStyle.WrapText"/> property.
        /// </summary>
        /// <param name="wrapText">Property value.</param>
        public CellTextFormatStyleBuilder SetWrapText(bool? wrapText)
        {
            _textFormat.WrapText = wrapText;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellTextFormatStyle.TextColor"/> property.
        /// </summary>
        /// <param name="color">Property value.</param>
        public CellTextFormatStyleBuilder SetTextColor(Color? color)
        {
            _textFormat.TextColor = color;
            return this;
        }

        /// <summary>
        /// Sets properties from another format.
        /// </summary>
        /// <param name="textFormat">Another format.</param>
        /// <param name="additionalTextFormat">Additional another format.</param>
        public CellTextFormatStyleBuilder SetFromFormat(
            CellTextFormatStyle textFormat,
            CellTextFormatStyle? additionalTextFormat = null)
        {
            return SetBold(textFormat.Bold ?? additionalTextFormat?.Bold)
                .SetItalic(textFormat.Italic ?? additionalTextFormat?.Italic)
                .SetFontFamily(textFormat.FontFamily ?? additionalTextFormat?.FontFamily)
                .SetTextColor(textFormat.TextColor ?? additionalTextFormat?.TextColor)
                .SetTextSize(textFormat.TextSize ?? additionalTextFormat?.TextSize)
                .SetWrapText(textFormat.WrapText ?? additionalTextFormat?.WrapText);
        }

        /// <summary>
        /// Returns the built <see cref="CellTextFormatStyle"/>.
        /// </summary>
        public CellTextFormatStyle Build()
        {
            return _textFormat;
        }
    }
}