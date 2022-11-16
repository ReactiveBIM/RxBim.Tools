namespace RxBim.Tools.TableBuilder.Builders;

using Styles;

/// <summary>
/// Builder for <see cref="CellContentMargins"/>.
/// </summary>
public interface ICellContentMarginsBuilder
{
    /// <summary>
    /// Sets the top margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    public ICellContentMarginsBuilder SetTopMargin(double? margin);

    /// <summary>
    /// Sets the right margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    public ICellContentMarginsBuilder SetRightMargin(double? margin);

    /// <summary>
    /// Sets the bottom margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    public ICellContentMarginsBuilder SetBottomMargin(double? margin);

    /// <summary>
    /// Sets the left margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    public ICellContentMarginsBuilder SetLeftMargin(double? margin);
}
