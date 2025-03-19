namespace RxBim.Tools.TableBuilder;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The builder of a single <see cref="Row"/> of a <see cref="Table"/>.
/// </summary>
internal class RowEditor : CellsSetEditor<Row, RowEditor>, IRowEditor
{
    /// <inheritdoc />
    public RowEditor(Row row)
        : base(row)
    {
    }

    /// <summary>
    /// Sets the height of the row.
    /// </summary>
    /// <param name="height">Row height value.</param>
    public IRowEditor SetHeight(double height)
    {
        if (height <= 0)
            throw new ArgumentException("Must be a positive number.", nameof(height));

        ObjectForBuild.Height = height;
        return this;
    }

    /// <inheritdoc />
    public IRowEditor MergeRow(Action<ICellEditor, ICellEditor>? action = null)
    {
        new CellEditor(ObjectForBuild.Cells.First()).MergeNext(ObjectForBuild.Cells.Count() - 1, action);
        return this;
    }

    /// <inheritdoc />
    IRowEditor IRowEditor.SetFormat(CellFormatStyle format)
    {
        SetFormat(format);
        return this;
    }

    /// <inheritdoc />
    IRowEditor IRowEditor.SetFormat(Action<ICellFormatStyleBuilder> action)
    {
        SetFormat(action);
        return this;
    }

    /// <inheritdoc />
    IRowEditor IRowEditor.FromList<TSource>(IList<TSource> source, Action<ICellEditor>? cellsAction)
    {
        FromList(source, cellsAction);
        return this;
    }

    /// <inheritdoc />
    public int GetRowIndex()
    {
        return ObjectForBuild.GetIndex();
    }
}