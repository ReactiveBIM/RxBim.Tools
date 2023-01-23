namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Builder for <see cref="CellContentMargins"/>.
/// </summary>
public interface ICellContentMarginsBuilder
{
    /// <summary>
    /// Sets the top margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetTopMargin(double? margin);

    /// <summary>
    /// Sets the right margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetRightMargin(double? margin);

    /// <summary>
    /// Sets the bottom margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetBottomMargin(double? margin);

    /// <summary>
    /// Sets the left margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetLeftMargin(double? margin);
}
