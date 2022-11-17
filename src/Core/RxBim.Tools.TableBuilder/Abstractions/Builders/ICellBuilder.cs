namespace RxBim.Tools.TableBuilder.Builders;

using System;
using Styles;

/// <summary>
/// The builder of a single <see cref="Cell"/> of a <see cref="Table"/>.
/// </summary>
public interface ICellBuilder
{
    /// <summary>
    /// Returns the object to be built.
    /// </summary>
    public object ObjectForBuild { get; }

    /// <summary>
    /// Returns new <see cref="TableBuilder"/> for <see cref="Table"/> to be build.
    /// </summary>
    public TableBuilder ToTable { get; }

    /// <summary>
    /// Returns a new <see cref="IRowBuilder"/> for the <see cref="Row"/> that the <see cref="Cell"/> is in.
    /// </summary>
    public IRowBuilder ToRow();

    /// <summary>
    /// Returns a new <see cref="IColumnBuilder"/> for the <see cref="Column"/> that the <see cref="Cell"/> is in.
    /// </summary>
    public IColumnBuilder ToColumn();

    /// <summary>
    /// Sets the content of the cell.
    /// </summary>
    /// <param name="data">A cell content object.</param>
    public ICellBuilder SetContent(ICellContent data);

    /// <summary>
    /// Sets the width of the cell.
    /// </summary>
    /// <param name="width">The width of the cell.</param>
    public ICellBuilder SetWidth(double width);

    /// <summary>
    /// Sets the height of the cell.
    /// </summary>
    /// <param name="height">The height of the cell.</param>
    public ICellBuilder SetHeight(double height);

    /// <summary>
    /// Set text in the cell.
    /// </summary>
    /// <param name="text">Text value.</param>
    public ICellBuilder SetText(string text);

    /// <summary>
    /// Set value in the cell.
    /// </summary>
    /// <param name="value">Value.</param>
    public ICellBuilder SetValue(object value);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the next cell to the right.
    /// </summary>
    /// <param name="step">Offset step to the right.</param>
    public ICellBuilder Next(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the next cell down.
    /// </summary>
    /// <param name="step">Down offset step.</param>
    public ICellBuilder Down(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the previous cell to the left.
    /// </summary>
    /// <param name="step">Offset step to the left.</param>
    public ICellBuilder Previous(int step = 1);

    /// <summary>
    /// Returns the <see cref="ICellBuilder"/> for the previous cell up.
    /// </summary>
    /// <param name="step">Up offset step.</param>
    public ICellBuilder Up(int step = 1);

    /// <summary>
    /// Merges cells horizontally to the right.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    /// <returns>Returns the <see cref="ICellBuilder"/> of the original cell.</returns>
    public ICellBuilder MergeNext(int count = 1, Action<ICellBuilder, ICellBuilder>? action = null);

    /// <summary>
    /// Merges cells vertically.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <param name="action">Delegate, applied to the cells to be merged.</param>
    /// <returns>Returns the <see cref="ICellBuilder"/> of the original cell.</returns>
    public ICellBuilder MergeDown(int count = 1, Action<ICellBuilder, ICellBuilder>? action = null);

    /// <summary>
    /// Merges cells horizontally to the left.
    /// </summary>
    /// <param name="count">The number of cells to merge.</param>
    /// <returns>Returns the <see cref="ICellBuilder"/> of the left cell of merged area.</returns>
    public ICellBuilder MergeLeft(int count = 1);

    /// <summary>
    /// Sets format for the cell.
    /// </summary>
    /// <param name="format">Format value.</param>
    public ICellBuilder SetFormat(CellFormatStyle format);

    /// <summary>
    /// Sets format for the cell.
    /// </summary>
    /// <param name="action">Format building action.</param>
    public ICellBuilder SetFormat(Action<ICellFormatStyleBuilder> action);
}
