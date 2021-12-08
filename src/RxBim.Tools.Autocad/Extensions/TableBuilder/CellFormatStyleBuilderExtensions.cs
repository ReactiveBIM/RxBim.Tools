namespace RxBim.Tools.Autocad.Extensions.TableBuilder
{
    using Tools.TableBuilder.Models.Styles;
    using Tools.TableBuilder.Services;

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
        public static CellFormatStyleBuilder SetContentMargins(
            this CellFormatStyleBuilder builder,
            double? verticalMargins,
            double? horizontalMargins)
        {
            return builder.SetContentMargins(verticalMargins, verticalMargins, horizontalMargins, horizontalMargins);
        }
    }
}