namespace RxBim.Tools.TableBuilder.Builders;

using System.Collections.Generic;

/// <summary>
/// The builder of a <see cref="CellsSet"/> of a <see cref="Table"/>.
/// </summary>
public interface ICellsSetBuilder<TItem>
    where TItem : TableItemBase
{
    /// <summary>
    /// Returns collection of <see cref="CellBuilder"/> for cells.
    /// </summary>
    public IEnumerable<ICellBuilder<TItem>> ToCells();
}


