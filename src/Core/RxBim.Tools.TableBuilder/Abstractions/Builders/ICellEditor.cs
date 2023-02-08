namespace RxBim.Tools.TableBuilder;

using System;

/// <summary>
/// The builder of a single <see cref="Cell"/> of a <see cref="Table"/>.
/// </summary>
public interface ICellEditor
{
    /// <summary>
    /// Returns a new <see cref="IRowEditor"/> for the <see cref="Row"/> that the <see cref="Cell"/> is in.
    /// </summary>
    IRowEditor ToRow();

    /// <summary>
    /// Returns a new <see cref="IColumnEditor"/> for the <see cref="Column"/> that the <see cref="Cell"/> is in.
    /// </summary>
    IColumnEditor ToColumn();

    /// <summary>
    /// Sets the content of the cell.
    /// </summary>
    /// <param name="data">A cell content object.</param>
    ICellEditor SetContent(ICellContent data);

    /// <summary>
    /// Sets the width of the cell.
    /// </summary>
    /// <param name="width">The width of the cell.</param>
    ICellEditor SetWidth(double width);

    /// <summary>
    /// Sets the height of the cell.
    /// </summary>
    /// <param name="height">The height of the cell.</param>
    ICellEditor SetHeight(double height);

    /// <summary>
    /// Set text in the cell.
    /// </summary>
    /// <param name="text">Text value.</param>
    ICellEditor SetText(string text);

    /// <summary>
    /// Set value in the cell.
    /// </summary>
    /// <param name="value">Value.</param>
    ICellEditor SetValue(object value);

    /// <summary>
    /// Returns the <see cref="ICellEditor"/> for the next cell to the right.
    /// </summary>
    /// <param name="step">Offset step to the right.</param>
    ICellEditor Next(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellEditor"/> for the next cell down.
    /// </summary>
    /// <param name="step">Down offset step.</param>
    ICellEditor Down(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellEditor"/> for the previous cell to the left.
    /// </summary>
    /// <param name="step">Offset step to the left.</param>
    ICellEditor Previous(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellEditor"/> for the previous cell up.
    /// </summary>
    /// <param name="step">Up offset step.</param>
    ICellEditor Up(int step = 1);

    /// <summary>
    /// Merges cells horizontally to the right.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    /// <returns>Returns the <see cref="ICellEditor"/> of the last merged cell.</returns>
    ICellEditor MergeNext(int count = 1, Action<ICellEditor, ICellEditor>? action = null);

    /// <summary>
    /// Merges cells vertically.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    /// <returns>Returns the <see cref="ICellEditor"/> of the last merged cell.</returns>
    ICellEditor MergeDown(int count = 1, Action<ICellEditor, ICellEditor>? action = null);

    /// <summary>
    /// Merges cells horizontally to the left.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <returns>Returns the <see cref="ICellEditor"/> of the left cell of merged area.</returns>
    ICellEditor MergeLeft(int count = 1);

    /// <summary>
    /// Sets format for the cell.
    /// </summary>
    /// <param name="format">Format value.</param>
    ICellEditor SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cell.
    /// </summary>
    /// <param name="action">Format building action.</param>
    ICellEditor SetFormat(Action<ICellFormatStyleBuilder> action);

    /// <summary>
    /// Returns the zero-based index of a cell in a table row.
    /// </summary>
    int GetColumnIndex();

    /// <summary>
    /// Returns the zero-based index of a cell in a table column.
    /// </summary>
    int GetRowIndex();
}
