namespace RxBim.Tools.TableBuilder.Builders;

using System;
using System.Collections.Generic;
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
    public IRowBuilder<TItem> SetHeight(double height);

    /// <summary>
    /// Merges all cells in the row.
    /// </summary>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    public IRowBuilder<TItem> MergeRow(Action<ICellBuilder<TItem>, ICellBuilder<TItem>>? action = null);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="format">Format value.</param>
    IRowBuilder<TItem> SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="action">Format building action.</param>
    IRowBuilder<TItem> SetFormat(Action<ICellFormatStyleBuilder> action);

    /// <summary>
    /// Fills cells with text values from list items.
    /// </summary>
    /// <param name="source">List of items.</param>
    /// <param name="cellsAction">Delegate. Applies to all filled cells.</param>
    /// <typeparam name="TSource">The type of the list item.</typeparam>
    public IRowBuilder<TItem> FromList<TSource>(IList<TSource> source, Action<ICellBuilder<TItem>>? cellsAction = null);
}
