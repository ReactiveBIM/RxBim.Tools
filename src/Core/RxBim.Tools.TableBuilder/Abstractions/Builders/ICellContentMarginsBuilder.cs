namespace RxBim.Tools.TableBuilder;

/// <summary>
/// Builder for <see cref="CellContentMargins"/>.
/// </summary>
public interface ICellContentMarginsBuilder : IBuilder<CellContentMargins>
{
    /// <summary>
    /// Sets the top margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetTop(double? margin);

    /// <summary>
    /// Sets the right margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetRight(double? margin);

    /// <summary>
    /// Sets the bottom margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetBottom(double? margin);

    /// <summary>
    /// Sets the left margin.
    /// </summary>
    /// <param name="margin">Margin value.</param>
    ICellContentMarginsBuilder SetLeft(double? margin);
}
