namespace RxBim.Tools.TableBuilder.Builders;

using System;
using Styles;

/// <summary>
/// The builder of a single <see cref="Column"/> of a <see cref="Table"/>.
/// </summary>
public interface IColumnBuilder<TItem> : ICellsSetBuilder<TItem>
    where TItem : TableItemBase
{
    /// <summary>
    /// Sets the width of the column.
    /// </summary>
    /// <param name="width">Column width.</param>
    IColumnBuilder<TItem> SetWidth(double width);
    
    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="format">Format value.</param>
    IColumnBuilder<TItem> SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="action">Format building action.</param>
    IColumnBuilder<TItem> SetFormat(Action<ICellFormatStyleBuilder> action);
}
