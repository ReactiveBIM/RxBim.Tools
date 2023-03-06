namespace RxBim.Tools.TableBuilder
{
    /// <summary>
    /// Extensions for <see cref="ICellFormatStyleBuilder"/>.
    /// </summary>
    public static class CellFormatStyleBuilderExtensions
    {
        /// <summary>
        /// Sets <see cref="CellFormatStyle.ContentMargins"/>
        /// </summary>
        /// <param name="builder"><see cref="CellFormatStyleBuilder"/> object.</param>
        /// <param name="top">Top margin value.</param>
        /// <param name="bottom">Bottom margin value.</param>
        /// <param name="left">Left margin value.</param>
        /// <param name="right">Right margin value.</param>
        public static ICellFormatStyleBuilder SetContentMargins(
            this ICellFormatStyleBuilder builder,
            double? top = null,
            double? bottom = null,
            double? left = null,
            double? right = null)
        {
            return builder.SetContentMargins(b => b
                .SetContentMargins(top, bottom, left, right));
        }

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
            return builder.SetContentMargins(b => b
                .SetContentMargins(verticalMargins, verticalMargins, horizontalMargins, horizontalMargins));
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.Borders"/>.
        /// </summary>
        /// <param name="builder"><see cref="ICellFormatStyleBuilder"/></param>
        /// <param name="top">Top border type.</param>
        /// <param name="bottom">Bottom border type.</param>
        /// <param name="left">Left border type.</param>
        /// <param name="right">Right border type.</param>
        public static ICellFormatStyleBuilder SetBorders(
            this ICellFormatStyleBuilder builder,
            CellBorderType? top = null,
            CellBorderType? bottom = null,
            CellBorderType? left = null,
            CellBorderType? right = null)
        {
            return builder.SetBorders(b => b.SetBorders(top, bottom, left, right));
        }

        /// <summary>
        /// Sets <see cref="CellFormatStyle.Borders"/>.
        /// </summary>
        /// <param name="builder"><see cref="ICellBordersBuilder"/></param>
        /// <param name="typeForAll"><see cref="CellBorderType"/> value.</param>
        public static ICellFormatStyleBuilder SetAllBorders(
            this ICellFormatStyleBuilder builder,
            CellBorderType? typeForAll)
        {
            return builder.SetBorders(b => b.SetAllBorders(typeForAll));
        }
    }
}
