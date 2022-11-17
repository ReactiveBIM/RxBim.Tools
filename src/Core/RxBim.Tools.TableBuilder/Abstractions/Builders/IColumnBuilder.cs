namespace RxBim.Tools.TableBuilder.Builders;

using System;

/// <summary>
/// The builder of a single <see cref="Column"/> of a <see cref="Table"/>.
/// </summary>
public interface IColumnBuilder
{
    /// <summary>
    /// Sets the width of the column.
    /// </summary>
    /// <param name="width">Column width.</param>
    public ColumnBuilder SetWidth(double width);
}
