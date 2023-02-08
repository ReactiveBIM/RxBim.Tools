namespace RxBim.Tools.TableBuilder;

using System;

/// <summary>
/// The builder of a single <see cref="Column"/> of a <see cref="Table"/>.
/// </summary>
public interface IColumnEditor : ICellsSetEditor
{
    /// <summary>
    /// Sets the width of the column.
    /// </summary>
    /// <param name="width">Column width.</param>
    IColumnEditor SetWidth(double width);
    
    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="format">Format value.</param>
    IColumnEditor SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cells set.
    /// </summary>
    /// <param name="action">Format building action.</param>
    IColumnEditor SetFormat(Action<ICellFormatStyleBuilder> action);
}
