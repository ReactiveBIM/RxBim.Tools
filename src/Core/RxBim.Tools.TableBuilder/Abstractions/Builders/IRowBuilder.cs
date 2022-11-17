namespace RxBim.Tools.TableBuilder.Builders;

using System;
using Styles;

/// <summary>
/// The builder of a single <see cref="Row"/> of a <see cref="Table"/>.
/// </summary>
public interface IRowBuilder<TItem> : ICellsSetBuilder<TItem>
    where TItem : TableItemBase
{
    /// <summary>
    /// Sets the height of the row.
    /// </summary>
    /// <param name="height">Row height value.</param>
    public IRowBuilder<Cell> SetHeight(double height);

    /// <summary>
    /// Merges all cells in the row.
    /// </summary>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    public IRowBuilder<Cell> MergeRow(Action<ICellBuilder<TItem>, ICellBuilder<TItem>>? action = null);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="format">Format value.</param>
    IRowBuilder<Cell> SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="action">Format building action.</param>
    IRowBuilder<Cell> SetFormat(Action<ICellFormatStyleBuilder> action);
}
