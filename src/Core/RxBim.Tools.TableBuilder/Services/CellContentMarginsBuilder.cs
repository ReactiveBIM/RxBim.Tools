namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Builder for <see cref="CellContentMargins"/>.
/// </summary>
internal class CellContentMarginsBuilder : ICellContentMarginsBuilder
{
    private readonly CellContentMargins _cellContentMargins;

    /// <summary>
    /// Initializes a new instance of the <see cref="CellContentMarginsBuilder"/> class.
    /// </summary>
    /// <param name="cellContentMargins"><see cref="CellContentMargins"/>.</param>
    public CellContentMarginsBuilder(CellContentMargins? cellContentMargins)
    {
        _cellContentMargins = cellContentMargins?.Copy() ?? new CellContentMargins();
    }

    /// <inheritdoc/>
    public ICellContentMarginsBuilder SetTop(double? margin)
    {
        _cellContentMargins.Top = margin;
        return this;
    }

    /// <inheritdoc/>
    public ICellContentMarginsBuilder SetRight(double? margin)
    {
        _cellContentMargins.Right = margin;
        return this;
    }

    /// <inheritdoc/>
    public ICellContentMarginsBuilder SetBottom(double? margin)
    {
        _cellContentMargins.Bottom = margin;
        return this;
    }

    /// <inheritdoc/>
    public ICellContentMarginsBuilder SetLeft(double? margin)
    {
        _cellContentMargins.Left = margin;
        return this;
    }

    /// <inheritdoc/>
    public CellContentMargins Build()
    {
        return _cellContentMargins.Copy();
    }
}
