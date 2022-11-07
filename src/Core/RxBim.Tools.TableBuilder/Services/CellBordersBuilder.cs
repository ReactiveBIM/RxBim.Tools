namespace RxBim.Tools.TableBuilder;

using Styles;

/// <summary>
/// Builder for <see cref="CellBorders"/>.
/// </summary>
public class CellBordersBuilder
{
    private readonly CellBorders _cellBorders;

    /// <summary>
    /// Initializes a new instance of the <see cref="CellBordersBuilder"/> class.
    /// </summary>
    /// <param name="cellBorders"><see cref="CellBorders"/></param>
    public CellBordersBuilder(CellBorders cellBorders)
    {
        _cellBorders = cellBorders;
    }

    /// <summary>
    /// Sets <see cref="CellFormatStyle.Borders"/>.
    /// </summary>
    /// <param name="top">Top border type.</param>
    /// <param name="bottom">Bottom border type.</param>
    /// <param name="left">Left border type.</param>
    /// <param name="right">Right border type.</param>
    public CellBordersBuilder SetBorders(
        CellBorderType? top = null,
        CellBorderType? bottom = null,
        CellBorderType? left = null,
        CellBorderType? right = null)
    {
        _cellBorders.Top = top;
        _cellBorders.Bottom = bottom;
        _cellBorders.Left = left;
        _cellBorders.Right = right;
        return this;
    }

    /// <summary>
    /// Sets <see cref="CellFormatStyle.Borders"/>.
    /// </summary>
    /// <param name="typeForAll"><see cref="CellBorderType"/> value.</param>
    public CellBordersBuilder SetAllBorders(CellBorderType? typeForAll)
    {
        _cellBorders.Top =
            _cellBorders.Bottom =
                _cellBorders.Left =
                    _cellBorders.Right = typeForAll;
        return this;
    }
}