namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Builder for <see cref="CellBorders"/>.
/// </summary>
public class CellBordersBuilder : ICellBordersBuilder
{
    private readonly CellBorders _cellBorders;

    /// <summary>
    /// Initializes a new instance of the <see cref="CellBordersBuilder"/> class.
    /// </summary>
    /// <param name="cellBorders"><see cref="CellBorders"/></param>
    public CellBordersBuilder(CellBorders? cellBorders = null)
    {
        _cellBorders = cellBorders?.Copy() ?? new CellBorders();
    }

    /// <inheritdoc/>
    public ICellBordersBuilder SetTop(CellBorderType? cellBorderType)
    {
        _cellBorders.Top = cellBorderType;
        return this;
    }

    /// <inheritdoc/>
    public ICellBordersBuilder SetRight(CellBorderType? cellBorderType)
    {
        _cellBorders.Right = cellBorderType;
        return this;
    }

    /// <inheritdoc/>
    public ICellBordersBuilder SetBottom(CellBorderType? cellBorderType)
    {
        _cellBorders.Bottom = cellBorderType;
        return this;
    }

    /// <inheritdoc/>
    public ICellBordersBuilder SetLeft(CellBorderType? cellBorderType)
    {
        _cellBorders.Left = cellBorderType;
        return this;
    }

    /// <inheritdoc/>
    public CellBorders Build()
    {
        return _cellBorders.Copy();
    }
}
