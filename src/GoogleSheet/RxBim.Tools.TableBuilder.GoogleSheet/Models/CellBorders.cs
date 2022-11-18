namespace RxBim.Tools.TableBuilder;

using JetBrains.Annotations;
using Styles;

/// <summary>
/// Model of cell borders.
/// </summary>
public class CellBorders
{
    /// <summary>
    /// Top border type.
    /// </summary>
    [UsedImplicitly]
    public CellBorderType Top { get; set; }

    /// <summary>
    /// Right border type.
    /// </summary>
    [UsedImplicitly]
    public CellBorderType Right { get; set; }

    /// <summary>
    /// Bottom border type.
    /// </summary>
    [UsedImplicitly]
    public CellBorderType Bottom { get; set; }

    /// <summary>
    /// Left border type.
    /// </summary>
    [UsedImplicitly]
    public CellBorderType Left { get; set; }
}