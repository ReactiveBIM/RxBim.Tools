namespace RxBim.Tools.TableBuilder;

using System;

/// <summary>
/// The builder of a single <see cref="Column"/> of a <see cref="Table"/>.
/// </summary>
internal class ColumnEditor : CellsSetEditor<Column, ColumnEditor>, IColumnEditor
{
    /// <inheritdoc />
    public ColumnEditor(Column column)
        : base(column)
    {
    }

    /// <summary>
    /// Sets the width of the column.
    /// </summary>
    /// <param name="width">Column width.</param>
    public IColumnEditor SetWidth(double width)
    {
        if (width <= 0)
            throw new ArgumentException("Must be a positive number.", nameof(width));

        ObjectForBuild.Width = width;
        return this;
    }

    /// <inheritdoc />
    IColumnEditor IColumnEditor.SetFormat(CellFormatStyle format)
    {
        SetFormat(format);
        return this;
    }

    /// <inheritdoc />
    IColumnEditor IColumnEditor.SetFormat(Action<ICellFormatStyleBuilder> action)
    {
        SetFormat(action);
        return this;
    }

    /// <inheritdoc />
    public int GetColumnIndex()
    {
        return ObjectForBuild.GetIndex();
    }
}