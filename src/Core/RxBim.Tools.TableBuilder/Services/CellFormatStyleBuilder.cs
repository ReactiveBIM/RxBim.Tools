namespace RxBim.Tools.TableBuilder
{
    using System;
    using System.Drawing;

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
            _format = format?.Copy() ?? new CellFormatStyle();
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetTextFormat(Action<ICellTextFormatStyleBuilder> action)
        {
            var builder = new CellTextFormatStyleBuilder(_format.TextFormat);
            action(builder);
            _format.TextFormat = builder.Build();
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetBorders(Action<ICellBordersBuilder> action)
        {
            var builder = new CellBordersBuilder(_format.Borders);
            action(builder);
            _format.Borders = builder.Build();
            return this;
        }

        /// <inheritdoc />
        public ICellFormatStyleBuilder SetContentMargins(Action<ICellContentMarginsBuilder> action)
        {
            var builder = new CellContentMarginsBuilder(_format.ContentMargins);
            action(builder);
            _format.ContentMargins = builder.Build();
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
        public ICellFormatStyleBuilder SetFromFormat(
            CellFormatStyle format,
            CellFormatStyle? defaultFormat = null,
            bool useNullValue = true)
        {
            SetBorders(bordersBuilder =>
            {
                SetValue(format.Borders.Top,
                    defaultFormat?.Borders.Top,
                    useNullValue,
                    v => bordersBuilder.SetTop(v));
                SetValue(format.Borders.Bottom,
                    defaultFormat?.Borders.Bottom,
                    useNullValue,
                    v => bordersBuilder.SetBottom(v));
                SetValue(format.Borders.Left,
                    defaultFormat?.Borders.Left,
                    useNullValue,
                    v => bordersBuilder.SetLeft(v));
                SetValue(format.Borders.Right,
                    defaultFormat?.Borders.Right,
                    useNullValue,
                    v => bordersBuilder.SetRight(v));
            });
            SetValue(
                format.BackgroundColor, defaultFormat?.BackgroundColor, useNullValue, v => SetBackgroundColor(v));
            SetContentMargins(contentMarginsBuilder =>
            {
                SetValue(format.ContentMargins.Top,
                    defaultFormat?.ContentMargins.Top,
                    useNullValue,
                    v => contentMarginsBuilder.SetTop(v));
                SetValue(format.ContentMargins.Bottom,
                    defaultFormat?.ContentMargins.Bottom,
                    useNullValue,
                    v => contentMarginsBuilder.SetBottom(v));
                SetValue(format.ContentMargins.Left,
                    defaultFormat?.ContentMargins.Left,
                    useNullValue,
                    v => contentMarginsBuilder.SetLeft(v));
                SetValue(format.ContentMargins.Right,
                    defaultFormat?.ContentMargins.Right,
                    useNullValue,
                    v => contentMarginsBuilder.SetRight(v));
            });
            SetValue(format.ContentHorizontalAlignment,
                defaultFormat?.ContentHorizontalAlignment,
                useNullValue,
                v => SetContentHorizontalAlignment(v));
            SetValue(format.ContentVerticalAlignment,
                defaultFormat?.ContentVerticalAlignment,
                useNullValue,
                v => SetContentVerticalAlignment(v));

            SetTextFormat(x => x.SetFromFormat(
                format.TextFormat, defaultFormat?.TextFormat, useNullValue));
            
            return this;
        }

        /// <inheritdoc />
        public CellFormatStyle Build()
        {
            return _format.Copy();
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