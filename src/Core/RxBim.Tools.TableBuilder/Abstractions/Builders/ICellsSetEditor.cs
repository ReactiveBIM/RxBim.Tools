namespace RxBim.Tools.TableBuilder;

using System.Collections.Generic;

/// <summary>
/// The builder of a <see cref="CellsSet"/> of a <see cref="Table"/>.
/// </summary>
public interface ICellsSetEditor
{
    /// <summary>
    /// Collection of <see cref="CellEditor"/> for cells.
    /// </summary>
    IEnumerable<ICellEditor> Cells { get; }
}
