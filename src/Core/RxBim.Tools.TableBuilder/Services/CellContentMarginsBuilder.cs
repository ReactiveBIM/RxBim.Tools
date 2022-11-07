namespace RxBim.Tools.TableBuilder;

using Styles;

/// <summary>
/// Builder for <see cref="CellContentMargins"/>.
/// </summary>
public class CellContentMarginsBuilder
{
    private readonly CellContentMargins _cellContentMargins;

    /// <summary>
    /// Initializes a new instance of the <see cref="CellContentMarginsBuilder"/> class.
    /// </summary>
    /// <param name="cellContentMargins"><see cref="CellContentMargins"/>.</param>
    public CellContentMarginsBuilder(CellContentMargins cellContentMargins)
    {
        _cellContentMargins = cellContentMargins;
    }

    /// <summary>
    /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
    /// </summary>
    /// <param name="top">Top margin value.</param>
    /// <param name="bottom">Bottom margin value.</param>
    /// <param name="left">Left margin value.</param>
    /// <param name="right">Right margin value.</param>
    public CellContentMarginsBuilder SetContentMargins(
        double? top = null,
        double? bottom = null,
        double? left = null,
        double? right = null)
    {
        _cellContentMargins.Top = top;
        _cellContentMargins.Bottom = bottom;
        _cellContentMargins.Left = left;
        _cellContentMargins.Right = right;

        return this;
    }

    /// <summary>
    /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
    /// </summary>
    /// <param name="marginsForAll">Margin value.</param>
    public CellContentMarginsBuilder SetContentAllMargins(double? marginsForAll = null)
    {
        _cellContentMargins.Top =
            _cellContentMargins.Bottom =
                _cellContentMargins.Left =
                    _cellContentMargins.Right = marginsForAll;
        return this;
    }
}