namespace RxBim.Tools.TableBuilder
{
    using Builders;
    using Styles;

    /// <summary>
    /// Extensions for <see cref="ICellFormatStyleBuilder"/>.
    /// </summary>
    public static class CellFormatStyleBuilderExtensions
    {
        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentMargins"/>
        /// </summary>
        /// <param name="builder"><see cref="CellFormatStyleBuilder"/> object.</param>
        /// <param name="verticalMargins">Vertical margins value.</param>
        /// <param name="horizontalMargins">Horizontal margins value.</param>
        public static ICellFormatStyleBuilder SetContentVerticalHorizontalMargins(
            this ICellFormatStyleBuilder builder,
            double? verticalMargins = null,
            double? horizontalMargins = null)
        {
            return builder.SetContentMargins(contentMarginsBuilder =>
                contentMarginsBuilder.SetContentMargins(verticalMargins, verticalMargins, horizontalMargins, horizontalMargins));
        }
    }
}
