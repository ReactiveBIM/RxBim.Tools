namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Extensions for <see cref="ICellBordersBuilder"/>
/// </summary>
public static class CellBorderBuilderExtensions
{
    /// <summary>
    /// Sets <see cref="CellFormatStyle.Borders"/>.
    /// </summary>
    /// <param name="bordersBuilder"><see cref="ICellBordersBuilder"/></param>
    /// <param name="top">Top border type.</param>
    /// <param name="bottom">Bottom border type.</param>
    /// <param name="left">Left border type.</param>
    /// <param name="right">Right border type.</param>
    public static ICellBordersBuilder SetBorders(
        this ICellBordersBuilder bordersBuilder,
        CellBorderType? top = null,
        CellBorderType? bottom = null,
        CellBorderType? left = null,
        CellBorderType? right = null)
    {
        bordersBuilder.SetTopBorder(top)
            .SetRightBorder(right)
            .SetLeftBorder(left)
            .SetBottomBorder(bottom);
        return bordersBuilder;
    }

    /// <summary>
    /// Sets <see cref="CellFormatStyle.Borders"/>.
    /// </summary>
    /// <param name="bordersBuilder"><see cref="ICellBordersBuilder"/></param>
    /// <param name="typeForAll"><see cref="CellBorderType"/> value.</param>
    public static ICellBordersBuilder SetAllBorders(
        this ICellBordersBuilder bordersBuilder,
        CellBorderType? typeForAll)
    {
        bordersBuilder
            .SetTopBorder(typeForAll)
            .SetRightBorder(typeForAll)
            .SetLeftBorder(typeForAll)
            .SetBottomBorder(typeForAll);
        return bordersBuilder;
    }
}
