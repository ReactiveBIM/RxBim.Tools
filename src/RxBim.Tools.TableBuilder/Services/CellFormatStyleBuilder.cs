namespace RxBim.Tools.TableBuilder.Services
{
    using System;
    using System.Drawing;
    using Models.Styles;

    /// <summary>
    /// Builder for <see cref="CellFormatStyle"/>.
    /// </summary>
    public class CellFormatStyleBuilder
    {
        private readonly CellFormatStyle _format;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellFormatStyleBuilder"/> class.
        /// </summary>
        /// <param name="format">Format for build.</param>
        public CellFormatStyleBuilder(CellFormatStyle? format = null)
        {
            _format = format ?? new CellFormatStyle();
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.TextFormat"/>.
        /// </summary>
        /// <param name="action">Action for text format.</param>
        public CellFormatStyleBuilder SetTextFormat(Action<CellTextFormatStyleBuilder> action)
        {
            var builder = new CellTextFormatStyleBuilder(_format.TextFormat);
            action(builder);
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.Borders"/>.
        /// </summary>
        /// <param name="top">Top border type.</param>
        /// <param name="bottom">Bottom border type.</param>
        /// <param name="left">Left border type.</param>
        /// <param name="right">Right border type.</param>
        public CellFormatStyleBuilder SetBorders(
            CellBorderType? top = null,
            CellBorderType? bottom = null,
            CellBorderType? left = null,
            CellBorderType? right = null)
        {
            _format.Borders.Top = top;
            _format.Borders.Bottom = bottom;
            _format.Borders.Left = left;
            _format.Borders.Right = right;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.Borders"/>.
        /// </summary>
        /// <param name="typeForAll"><see cref="CellBorderType"/> value.</param>
        public CellFormatStyleBuilder SetAllBorders(CellBorderType? typeForAll)
        {
            _format.Borders.Top =
                _format.Borders.Bottom =
                    _format.Borders.Left =
                        _format.Borders.Right = typeForAll;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
        /// </summary>
        /// <param name="top">Top margin value.</param>
        /// <param name="bottom">Bottom margin value.</param>
        /// <param name="left">Left margin value.</param>
        /// <param name="right">Right margin value.</param>
        public CellFormatStyleBuilder SetContentMargins(
            double? top = null,
            double? bottom = null,
            double? left = null,
            double? right = null)
        {
            _format.ContentMargins.Top = top;
            _format.ContentMargins.Bottom = bottom;
            _format.ContentMargins.Left = left;
            _format.ContentMargins.Right = right;

            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
        /// </summary>
        /// <param name="marginsForAll">Margin value.</param>
        public CellFormatStyleBuilder SetContentAllMargins(double? marginsForAll = null)
        {
            _format.ContentMargins.Top =
                _format.ContentMargins.Bottom =
                    _format.ContentMargins.Left =
                        _format.ContentMargins.Right = marginsForAll;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.BackgroundColor"/> property.
        /// </summary>
        /// <param name="color">Property value.</param>
        public CellFormatStyleBuilder SetBackgroundColor(Color? color = null)
        {
            _format.BackgroundColor = color;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentHorizontalAlignment"/> property.
        /// </summary>
        /// <param name="alignment">Property value.</param>
        public CellFormatStyleBuilder SetContentHorizontalAlignment(CellContentHorizontalAlignment? alignment = null)
        {
            _format.ContentHorizontalAlignment = alignment;
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentVerticalAlignment"/> property.
        /// </summary>
        /// <param name="alignment">Property value.</param>
        public CellFormatStyleBuilder SetContentVerticalAlignment(CellContentVerticalAlignment? alignment = null)
        {
            _format.ContentVerticalAlignment = alignment;
            return this;
        }

        /// <summary>
        /// Sets properties from another format.
        /// </summary>
        /// <param name="format">Another format.</param>
        /// <param name="additionalFormat">Additional another format.</param>
        public CellFormatStyleBuilder SetFromFormat(CellFormatStyle format, CellFormatStyle? additionalFormat = null)
        {
            return SetBorders(
                    format.Borders.Top ?? additionalFormat?.Borders.Top,
                    format.Borders.Bottom ?? additionalFormat?.Borders.Bottom,
                    format.Borders.Left ?? additionalFormat?.Borders.Left,
                    format.Borders.Right ?? additionalFormat?.Borders.Right)
                .SetBackgroundColor(format.BackgroundColor ?? additionalFormat?.BackgroundColor)
                .SetContentMargins(
                    format.ContentMargins.Top ?? additionalFormat?.ContentMargins.Top,
                    format.ContentMargins.Bottom ?? additionalFormat?.ContentMargins.Bottom,
                    format.ContentMargins.Left ?? additionalFormat?.ContentMargins.Left,
                    format.ContentMargins.Right ?? additionalFormat?.ContentMargins.Right)
                .SetContentHorizontalAlignment(
                    format.ContentHorizontalAlignment ?? additionalFormat?.ContentHorizontalAlignment)
                .SetContentVerticalAlignment(
                    format.ContentVerticalAlignment ?? additionalFormat?.ContentVerticalAlignment)
                .SetTextFormat(x => x.SetFromFormat(format.TextFormat, additionalFormat?.TextFormat));
        }

        /// <summary>
        /// Returns the built <see cref="CellFormatStyle"/>.
        /// </summary>
        public CellFormatStyle Build()
        {
            return _format;
        }
    }
}