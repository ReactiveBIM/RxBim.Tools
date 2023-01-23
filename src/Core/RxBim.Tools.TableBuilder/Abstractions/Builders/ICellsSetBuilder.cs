namespace RxBim.Tools.TableBuilder;

using System.Collections.Generic;

/// <summary>
/// The builder of a <see cref="CellsSet"/> of a <see cref="Table"/>.
/// </summary>
public interface ICellsSetBuilder
{
    /// <summary>
    /// Collection of <see cref="CellBuilder"/> for cells.
    /// </summary>
    IEnumerable<ICellBuilder> Cells { get; }
}
