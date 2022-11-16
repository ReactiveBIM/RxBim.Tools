namespace RxBim.Tools.TableBuilder
{
    using System.Drawing;
    using Builders;
    using JetBrains.Annotations;
    using Styles;

    /// <summary>
    /// Builder for <see cref="CellTextFormatStyle"/>
    /// </summary>
    [PublicAPI]
    public class CellTextFormatStyleBuilder : ICellTextFormatStyleBuilder
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

        /// <inheritdoc />
        public ICellTextFormatStyleBuilder SetFontFamily(string? fontFamily)
        {
            _textFormat.FontFamily = fontFamily;
            return this;
        }

        /// <inheritdoc />
        public ICellTextFormatStyleBuilder SetBold(bool? bold)
        {
            _textFormat.Bold = bold;
            return this;
        }

        /// <inheritdoc />
        public ICellTextFormatStyleBuilder SetItalic(bool? italic)
        {
            _textFormat.Italic = italic;
            return this;
        }

        /// <inheritdoc />
        public ICellTextFormatStyleBuilder SetTextSize(double? textSize)
        {
            _textFormat.TextSize = textSize;
            return this;
        }

        /// <inheritdoc />
        public ICellTextFormatStyleBuilder SetWrapText(bool? wrapText)
        {
            _textFormat.WrapText = wrapText;
            return this;
        }

        /// <inheritdoc />
        public ICellTextFormatStyleBuilder SetTextColor(Color? color)
        {
            _textFormat.TextColor = color;
            return this;
        }

        /// <inheritdoc />
        public ICellTextFormatStyleBuilder SetFromFormat(
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

        /// <inheritdoc />
        public CellTextFormatStyle Build()
        {
            return _textFormat;
        }
    }
}
