namespace RxBim.Tools.TableBuilder;

using Builders;
using Styles;

/// <summary>
/// Extensions for <see cref="ICellContentMarginsBuilder"/>.
/// </summary>
public static class CellContentMarginsBuilderExtensions
{
    /// <summary>
    /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
    /// </summary>
    /// <param name="cellContentMarginsBuilder"><see cref="ICellContentMarginsBuilder"/></param>
    /// <param name="top">Top margin value.</param>
    /// <param name="bottom">Bottom margin value.</param>
    /// <param name="left">Left margin value.</param>
    /// <param name="right">Right margin value.</param>
    public static ICellContentMarginsBuilder SetContentMargins(
        this ICellContentMarginsBuilder cellContentMarginsBuilder,
        double? top = null,
        double? bottom = null,
        double? left = null,
        double? right = null)
    {
        cellContentMarginsBuilder
            .SetBottomMargin(bottom)
            .SetLeftMargin(left)
            .SetRightMargin(right)
            .SetTopMargin(top);
        return cellContentMarginsBuilder;
    }

    /// <summary>
    /// Sets <see cref="CellFormatStyle.ContentMargins"/>.
    /// </summary>
    /// <param name="cellContentMarginsBuilder"><see cref="ICellContentMarginsBuilder"/></param>
    /// <param name="marginsForAll">Margin value.</param>
    public static ICellContentMarginsBuilder SetAllMargins(
        this ICellContentMarginsBuilder cellContentMarginsBuilder,
        double? marginsForAll = null)
    {
        cellContentMarginsBuilder
            .SetBottomMargin(marginsForAll)
            .SetLeftMargin(marginsForAll)
            .SetRightMargin(marginsForAll)
            .SetTopMargin(marginsForAll);
        return cellContentMarginsBuilder;
    }
}
