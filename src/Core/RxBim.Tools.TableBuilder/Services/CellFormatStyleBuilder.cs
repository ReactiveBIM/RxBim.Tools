namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Drawing;
    using Builders;
    using Styles;

    /// <summary>
    /// Builder for <see cref="CellFormatStyle"/>.
    /// </summary>
    public class CellFormatStyleBuilder : ICellFormatStyleBuilder
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

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetTextFormat(Action<ICellTextFormatStyleBuilder> action)
        {
            var builder = new CellTextFormatStyleBuilder(_format.TextFormat);
            action(builder);
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetBorders(Action<ICellBordersBuilder> action)
        {
            var builder = new CellBordersBuilder(_format.Borders);
            action(builder);
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetContentMargins(Action<ICellContentMarginsBuilder> action)
        {
            var builder = new CellContentMarginsBuilder(_format.ContentMargins);
            action(builder);
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetBackgroundColor(Color? color = null)
        {
            _format.BackgroundColor = color;
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetContentHorizontalAlignment(CellContentHorizontalAlignment? alignment = null)
        {
            _format.ContentHorizontalAlignment = alignment;
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetContentVerticalAlignment(CellContentVerticalAlignment? alignment = null)
        {
            _format.ContentVerticalAlignment = alignment;
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetFromFormat(CellFormatStyle format, CellFormatStyle? defaultFormat = null)
        {
            return SetBorders(bordersBuilder => bordersBuilder.SetBorders(
                    format.Borders.Top ?? defaultFormat?.Borders.Top,
                    format.Borders.Bottom ?? defaultFormat?.Borders.Bottom,
                    format.Borders.Left ?? defaultFormat?.Borders.Left,
                    format.Borders.Right ?? defaultFormat?.Borders.Right))
                .SetBackgroundColor(format.BackgroundColor ?? defaultFormat?.BackgroundColor)
                .SetContentMargins(contentMarginsBuilder =>
                    contentMarginsBuilder.SetContentMargins(
                        format.ContentMargins.Top ?? defaultFormat?.ContentMargins.Top,
                        format.ContentMargins.Bottom ?? defaultFormat?.ContentMargins.Bottom,
                        format.ContentMargins.Left ?? defaultFormat?.ContentMargins.Left,
                        format.ContentMargins.Right ?? defaultFormat?.ContentMargins.Right))
                .SetContentHorizontalAlignment(
                    format.ContentHorizontalAlignment ?? defaultFormat?.ContentHorizontalAlignment)
                .SetContentVerticalAlignment(
                    format.ContentVerticalAlignment ?? defaultFormat?.ContentVerticalAlignment)
                .SetTextFormat(x => x.SetFromFormat(format.TextFormat, defaultFormat?.TextFormat));
        }

        /// <inheritdoc />
        public CellFormatStyle Build()
        {
            return _format;
        }
    }
}
