namespace RxBim.Tools.TableBuilder;

using System;
using System.Collections.Generic;

/// <summary>
/// The builder of a single <see cref="Row"/> of a <see cref="Table"/>.
/// </summary>
public interface IRowBuilder : ICellsSetBuilder
{
    /// <summary>
    /// Sets the height of the row.
    /// </summary>
    /// <param name="height">Row height value.</param>
    IRowBuilder SetHeight(double height);

    /// <summary>
    /// Merges all cells in the row.
    /// </summary>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    IRowBuilder MergeRow(Action<ICellBuilder, ICellBuilder>? action = null);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="format">Format value.</param>
    IRowBuilder SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="action">Format building action.</param>
    IRowBuilder SetFormat(Action<ICellFormatStyleBuilder> action);

    /// <summary>
    /// Fills cells with text values from list items.
    /// </summary>
    /// <param name="source">List of items.</param>
    /// <param name="cellsAction">Delegate. Applies to all filled cells.</param>
    /// <typeparam name="TSource">The type of the list item.</typeparam>
    IRowBuilder FromList<TSource>(IList<TSource> source, Action<ICellBuilder>? cellsAction = null);
}
