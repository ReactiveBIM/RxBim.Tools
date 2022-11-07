namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Drawing;
    using Styles;

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
        /// <param name="action">Action for text format.</param>
        public CellFormatStyleBuilder SetBorders(Action<CellBordersBuilder> action)
        {
            var builder = new CellBordersBuilder(_format.Borders);
            action(builder);
            return this;
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
        /// </summary>
        /// <param name="action">Action for text format.</param>
        public CellFormatStyleBuilder SetContentMargins(Action<CellContentMarginsBuilder> action)
        {
            var builder = new CellContentMarginsBuilder(_format.ContentMargins);
            action(builder);
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
            return SetBorders(bordersBuilder => bordersBuilder.SetBorders(
                    format.Borders.Top ?? additionalFormat?.Borders.Top,
                    format.Borders.Bottom ?? additionalFormat?.Borders.Bottom,
                    format.Borders.Left ?? additionalFormat?.Borders.Left,
                    format.Borders.Right ?? additionalFormat?.Borders.Right))
                .SetBackgroundColor(format.BackgroundColor ?? additionalFormat?.BackgroundColor)
                .SetContentMargins(contentMarginsBuilder =>
                    contentMarginsBuilder.SetContentMargins(
                        format.ContentMargins.Top ?? additionalFormat?.ContentMargins.Top,
                        format.ContentMargins.Bottom ?? additionalFormat?.ContentMargins.Bottom,
                        format.ContentMargins.Left ?? additionalFormat?.ContentMargins.Left,
                        format.ContentMargins.Right ?? additionalFormat?.ContentMargins.Right))
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

        /// <summary>
        /// Modify format
        /// </summary>
        /// <param name="action">The modify action.</param>
        public void ModifyFormat(Action<CellFormatStyle> action)
        {
            action(_format);
        }
    }
}
