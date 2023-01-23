namespace RxBim.Tools.TableBuilder;

using System;

/// <summary>
/// The builder of a single <see cref="Cell"/> of a <see cref="Table"/>.
/// </summary>
public interface ICellBuilder
{
    /// <summary>
    /// Returns the object to be built.
    /// </summary>
    Cell ObjectForBuild { get; }

    /// <summary>
    /// Returns new <see cref="TableBuilder"/> for <see cref="Table"/> to be build.
    /// </summary>
    TableBuilder ToTable();

    /// <summary>
    /// Returns a new <see cref="IRowBuilder"/> for the <see cref="Row"/> that the <see cref="Cell"/> is in.
    /// </summary>
    IRowBuilder ToRow();

    /// <summary>
    /// Returns a new <see cref="IColumnBuilder"/> for the <see cref="Column"/> that the <see cref="Cell"/> is in.
    /// </summary>
    IColumnBuilder ToColumn();

    /// <summary>
    /// Sets the content of the cell.
    /// </summary>
    /// <param name="data">A cell content object.</param>
    ICellBuilder SetContent(ICellContent data);

    /// <summary>
    /// Sets the width of the cell.
    /// </summary>
    /// <param name="width">The width of the cell.</param>
    ICellBuilder SetWidth(double width);

    /// <summary>
    /// Sets the height of the cell.
    /// </summary>
    /// <param name="height">The height of the cell.</param>
    ICellBuilder SetHeight(double height);

    /// <summary>
    /// Set text in the cell.
    /// </summary>
    /// <param name="text">Text value.</param>
    ICellBuilder SetText(string text);

    /// <summary>
    /// Set value in the cell.
    /// </summary>
    /// <param name="value">Value.</param>
    ICellBuilder SetValue(object value);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the next cell to the right.
    /// </summary>
    /// <param name="step">Offset step to the right.</param>
    ICellBuilder Next(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the next cell down.
    /// </summary>
    /// <param name="step">Down offset step.</param>
    ICellBuilder Down(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the previous cell to the left.
    /// </summary>
    /// <param name="step">Offset step to the left.</param>
    ICellBuilder Previous(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the previous cell up.
    /// </summary>
    /// <param name="step">Up offset step.</param>
    ICellBuilder Up(int step = 1);

    /// <summary>
    /// Merges cells horizontally to the right.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    /// <returns>Returns the <see cref="ICellBuilder"/> of the original cell.</returns>
    ICellBuilder MergeNext(int count = 1, Action<ICellBuilder, ICellBuilder>? action = null);

    /// <summary>
    /// Merges cells vertically.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    /// <returns>Returns the <see cref="ICellBuilder"/> of the original cell.</returns>
    ICellBuilder MergeDown(int count = 1, Action<ICellBuilder, ICellBuilder>? action = null);

    /// <summary>
    /// Merges cells horizontally to the left.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <returns>Returns the <see cref="ICellBuilder"/> of the left cell of merged area.</returns>
    ICellBuilder MergeLeft(int count = 1);

    /// <summary>
    /// Sets format for the cell.
    /// </summary>
    /// <param name="format">Format value.</param>
    ICellBuilder SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cell.
    /// </summary>
    /// <param name="action">Format building action.</param>
    ICellBuilder SetFormat(Action<ICellFormatStyleBuilder> action);
}
