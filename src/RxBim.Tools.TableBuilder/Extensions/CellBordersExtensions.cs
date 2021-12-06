namespace RxBim.Tools.TableBuilder.Extensions
{
    using Models.Styles;

    /// <summary>
    /// Extensions for <see cref="CellBorders"/>
    /// </summary>
    public static class CellBordersExtensions
    {
        /// <summary>
        /// Sets borders.
        /// </summary>
        /// <param name="borders"><see cref="CellBorders"/> object.</param>
        /// <param name="top"><see cref="CellBorders.Top"/> value.</param>
        /// <param name="bottom"><see cref="CellBorders.Bottom"/> value.</param>
        /// <param name="left"><see cref="CellBorders.Left"/> value.</param>
        /// <param name="right"><see cref="CellBorders.Right"/> value.</param>
        public static void SetBorders(
            this CellBorders borders,
            CellBorderType top = CellBorderType.Thin,
            CellBorderType bottom = CellBorderType.Thin,
            CellBorderType left = CellBorderType.Thin,
            CellBorderType right = CellBorderType.Thin)
        {
            borders.Top = top;
            borders.Bottom = bottom;
            borders.Left = left;
            borders.Right = right;
        }

        /// <summary>
        /// Sets borders.
        /// </summary>
        /// <param name="borders"><see cref="CellBorders"/> object.</param>
        /// <param name="typeForAll">The type of line for all borders.</param>
        public static void SetAllBorders(this CellBorders borders, CellBorderType typeForAll = CellBorderType.Thin)
        {
            borders.Top = borders.Bottom = borders.Left = borders.Right = typeForAll;
        }
    }
}