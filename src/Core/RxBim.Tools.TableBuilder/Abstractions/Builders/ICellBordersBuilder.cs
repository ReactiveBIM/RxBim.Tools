namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Builder for <see cref="CellBorders"/>.
/// </summary>
public interface ICellBordersBuilder : IBuilder<CellBorders>
{
    /// <summary>
    /// Sets the top border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    ICellBordersBuilder SetTop(CellBorderType? cellBorderType);

    /// <summary>
    /// Sets the right border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    ICellBordersBuilder SetRight(CellBorderType? cellBorderType);

    /// <summary>
    /// Sets the bottom border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    ICellBordersBuilder SetBottom(CellBorderType? cellBorderType);

    /// <summary>
    /// Sets the left border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    ICellBordersBuilder SetLeft(CellBorderType? cellBorderType);
}
