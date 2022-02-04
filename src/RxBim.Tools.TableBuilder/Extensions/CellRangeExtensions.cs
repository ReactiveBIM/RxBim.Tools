namespace RxBim.Tools.TableBuilder.Extensions
{
    using Models;

    /// <summary>
    /// Extensions for <see cref="CellRange"/>
    /// </summary>
    internal static class CellRangeExtensions
    {
        /// <summary>
        /// Returns true if this range (<paramref name="range"/>) is within another range (<paramref name="outerRange"/>)
        /// </summary>
        /// <param name="range">This <see cref="CellRange"/>.</param>
        /// <param name="outerRange">Another <see cref="CellRange"/>.</param>
        public static bool IsInsideFor(this CellRange range, CellRange outerRange)
        {
            return range.TopRow >= outerRange.TopRow &&
                   range.BottomRow <= outerRange.BottomRow &&
                   range.LeftColumn >= outerRange.LeftColumn &&
                   range.RightColumn <= outerRange.RightColumn;
        }

        /// <summary>
        /// Returns true if this range crosses another range, or if one of them includes another.
        /// </summary>
        /// <param name="range">This <see cref="CellRange"/></param>
        /// <param name="otherRange">Another <see cref="CellRange"/></param>
        public static bool IsIntersectWith(this CellRange range, CellRange otherRange)
        {
            return otherRange.RightColumn >= range.LeftColumn &&
                   otherRange.LeftColumn <= range.RightColumn &&
                   otherRange.BottomRow >= range.TopRow &&
                   otherRange.TopRow <= range.BottomRow;
        }
    }
}