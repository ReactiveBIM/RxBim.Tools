namespace RxBim.Tools.TableBuilder.Builders;

using Styles;

/// <summary>
/// Builder for <see cref="CellBorders"/>.
/// </summary>
public interface ICellBordersBuilder
{
    /// <summary>
    /// Sets the top border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    public ICellBordersBuilder SetTopBorder(CellBorderType? cellBorderType);

    /// <summary>
    /// Sets the right border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    public ICellBordersBuilder SetRightBorder(CellBorderType? cellBorderType);

    /// <summary>
    /// Sets the bottom border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    public ICellBordersBuilder SetBottomBorder(CellBorderType? cellBorderType);

    /// <summary>
    /// Sets the left border.
    /// </summary>
    /// <param name="cellBorderType"><see cref="CellBorderType"/></param>
    public ICellBordersBuilder SetLeftBorder(CellBorderType? cellBorderType);
}
