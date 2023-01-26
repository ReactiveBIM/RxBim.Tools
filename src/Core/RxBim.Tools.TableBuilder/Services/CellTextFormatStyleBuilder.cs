namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Drawing;
    using JetBrains.Annotations;

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
            CellTextFormatStyle? additionalTextFormat = null,
            bool useNullValue = true)
        {
            SetValue(textFormat.Bold, additionalTextFormat?.Bold, useNullValue, v => SetBold(v));
            SetValue(textFormat.Italic, additionalTextFormat?.Italic, useNullValue, v => SetItalic(v));
            SetValue(textFormat.FontFamily, additionalTextFormat?.FontFamily, useNullValue, v => SetFontFamily(v));
            SetValue(textFormat.TextColor, additionalTextFormat?.TextColor, useNullValue, v => SetTextColor(v));
            SetValue(textFormat.TextSize, additionalTextFormat?.TextSize, useNullValue, v => SetTextSize(v));
            SetValue(textFormat.WrapText, additionalTextFormat?.WrapText, useNullValue, v => SetWrapText(v));
            return this;
        }

        /// <inheritdoc />
        public CellTextFormatStyle Build()
        {
            return _textFormat;
        }

        private static void SetValue<T>(T? main, T? alternative, bool useNullValue, Action<T?> setValueAction)
        {
            var value = main ?? alternative;
            if (main is not null || useNullValue)
            {
                setValueAction(value);
            }
        }
    }
}
