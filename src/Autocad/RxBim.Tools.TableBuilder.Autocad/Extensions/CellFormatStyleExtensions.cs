namespace RxBim.Tools.TableBuilder
{
    using Styles;

    /// <summary>
    /// Extensions for <see cref="CellFormatStyle"/>
    /// </summary>
    public static class CellFormatStyleExtensions
    {
        /// <summary>
        /// Returns horizontal margins value.
        /// </summary>
        /// <param name="format"><see cref="CellFormatStyle"/> object.</param>
        public static double? GetContentHorizontalMargins(this CellFormatStyle format)
        {
            return format.ContentMargins.Left ?? format.ContentMargins.Right ?? 0;
        }

        /// <summary>
        /// Returns vertical margins value.
        /// </summary>
        /// <param name="format"><see cref="CellFormatStyle"/> object.</param>
        public static double? GetContentVerticalMargins(this CellFormatStyle format)
        {
            return format.ContentMargins.Top ?? format.ContentMargins.Bottom ?? 0;
        }
    }
}