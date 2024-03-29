﻿namespace RxBim.Tools.TableBuilder;

using System;
using System.Collections.Generic;

/// <summary>
/// The builder of a single <see cref="Row"/> of a <see cref="Table"/>.
/// </summary>
public interface IRowEditor : ICellsSetEditor
{
    /// <summary>
    /// Sets the height of the row.
    /// </summary>
    /// <param name="height">Row height value.</param>
    IRowEditor SetHeight(double height);

    /// <summary>
    /// Merges all cells in the row.
    /// </summary>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    IRowEditor MergeRow(Action<ICellEditor, ICellEditor>? action = null);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="format">Format value.</param>
    IRowEditor SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="action">Format building action.</param>
    IRowEditor SetFormat(Action<ICellFormatStyleBuilder> action);

    /// <summary>
    /// Fills cells with text values from list items.
    /// </summary>
    /// <param name="source">List of items.</param>
    /// <param name="cellsAction">Delegate. Applies to all filled cells.</param>
    /// <typeparam name="TSource">The type of the list item.</typeparam>
    IRowEditor FromList<TSource>(IList<TSource> source, Action<ICellEditor>? cellsAction = null);

    /// <summary>
    /// Returns the index of this row in the table.
    /// </summary>
    int GetRowIndex();
}
