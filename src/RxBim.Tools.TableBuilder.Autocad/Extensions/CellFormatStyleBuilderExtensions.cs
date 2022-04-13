namespace RxBim.Tools.TableBuilder
{
    using Styles;

    /// <summary>
    /// Extensions for <see cref="CellFormatStyleBuilder"/>
    /// </summary>
    public static class CellFormatStyleBuilderExtensions
    {
        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentMargins"/>
        /// </summary>
        /// <param name="builder"><see cref="CellFormatStyleBuilder"/> object.</param>
        /// <param name="verticalMargins">Vertical margins value.</param>
        /// <param name="horizontalMargins">Horizontal margins value.</param>
        public static CellFormatStyleBuilder SetContentVerticalHorizontalMargins(
            this CellFormatStyleBuilder builder,
            double? verticalMargins = null,
            double? horizontalMargins = null)
        {
            return builder.SetContentMargins(verticalMargins, verticalMargins, horizontalMargins, horizontalMargins);
        }
    }
}