namespace RxBim.Tools.TableBuilder.Builders;

using System;
using System.Collections.Generic;
using Styles;

/// <summary>
/// The builder of a <see cref="Table"/>.
/// </summary>
public interface ITableBuilder<TItem>
    where TItem : TableItemBase
{
    /// <summary>
    /// Returns the builder of a table cell by row and column index.
    /// </summary>
    /// <param name="row">Row index.</param>
    /// <param name="column">Column index.</param>
    public ICellBuilder<TItem> this[int row, int column] { get; }

    /// <summary>
    /// Returns collection of <see cref="RowBuilder"/> for table rows.
    /// </summary>
    public IEnumerable<IRowBuilder<TItem>> ToRows();

    /// <summary>
    /// Returns collection of <see cref="ColumnBuilder"/> for table rows.
    /// </summary>
    public IEnumerable<IColumnBuilder<TItem>> GetColumns();

    /// <summary>
    /// Sets the default format for all cells.
    /// </summary>
    /// <param name="formatStyle">Format value.</param>
    /// <returns>
    /// Used if the cell doesn't have its own format and the default format is not set with a row or column.
    /// </returns>
    public ITableBuilder<TItem> SetFormat(CellFormatStyle formatStyle);

    /// <summary>
    /// Sets format for the table.
    /// </summary>
    /// <param name="action">Format building action.</param>
    public ITableBuilder<TItem> SetFormat(Action<ICellFormatStyleBuilder> action);

    /// <summary>
    /// Sets the format for a range of cells.
    /// </summary>
    /// <param name="formatStyle">Format value.</param>
    /// <param name="startRow">The starting row of the range of cells.</param>
    /// <param name="startColumn">The starting column of the range of cells.</param>
    /// <param name="rangeWidth">The width of the range of cells (number of columns).</param>
    /// <param name="rangeHeight">The height of the range of cells (number of rows).</param>
    public ITableBuilder<TItem> SetCellsFormat(
        CellFormatStyle formatStyle,
        int startRow,
        int startColumn,
        int rangeWidth,
        int rangeHeight);

    /// <summary>
    /// Sets the standard format for the table.
    /// </summary>
    /// <param name="headerRowsCount">Table header rows count.</param>
    /// <remarks>
    /// All text centered, all header lines bold.
    /// All horizontal borders between table rows are of standard thickness.
    /// </remarks>
    public ITableBuilder<TItem> SetTableStateStandardFormat(int headerRowsCount);

    /// <summary>
    /// Sets the width of the table.
    /// </summary>
    /// <param name="width">Width value/</param>
    public ITableBuilder<TItem> SetWidth(double width);

    /// <summary>
    /// Sets the height of the table.
    /// </summary>
    /// <param name="height">Height value/</param>
    public ITableBuilder<TItem> SetHeight(double height);

    /// <summary>
    /// Creates and adds rows to the table.
    /// </summary>
    /// <param name="action">The action to be taken on new rows.</param>
    /// <param name="count">The number of rows to add.</param>
    public ITableBuilder<TItem> AddRow(Action<IRowBuilder<TItem>>? action = null, int count = 1);

    /// <summary>
    /// Fills table rows with values from a list.
    /// </summary>
    /// <param name="source">
    /// A list is a source of values. The properties retrieved by the <paramref name="propertySelectors"/>
    /// parameter of each individual item in this list will end up on a separate row.
    /// </param>
    /// <param name="rowIndex">The index of the initial row to fill.</param>
    /// <param name="columnIndex">The index of the initial column to fill.</param>
    /// <param name="propertySelectors">
    /// Functions for getting values from a source list.
    /// Each separate function retrieves the value for the cells in a particular column.
    /// </param>
    /// <typeparam name="TSource">The type of object in the source list.</typeparam>
    public ITableBuilder<TItem> AddRowsFromList<TSource>(
        IEnumerable<TSource> source,
        int rowIndex,
        int columnIndex,
        params Func<TSource, object>[] propertySelectors);

    /// <summary>
    /// Creates and adds columns to the table.
    /// </summary>
    /// <param name="action">The action to be taken on new columns.</param>
    /// <param name="count">The number of columns to add.</param>
    public ITableBuilder<TItem> AddColumn(Action<IColumnBuilder<TItem>>? action = null, int count = 1);

    /// <summary>
    /// Fills table columns with values from a list.
    /// </summary>
    /// <param name="source">
    ///     A list is a source of values. The properties retrieved by the <paramref name="propertySelectors"/>
    ///     parameter of each individual item in this list will end up on a separate column.
    /// </param>
    /// <param name="rowIndex">The index of the initial row to fill.</param>
    /// <param name="columnIndex">The index of the initial column to fill.</param>
    /// <param name="propertySelectors">
    ///     Functions for getting values from a source list.
    ///     Each separate function retrieves the value for the cells in a particular row.
    /// </param>
    /// <typeparam name="TSource">The type of object in the source list.</typeparam>
    public ITableBuilder<TItem> AddColumnsFromList<TSource>(
        IEnumerable<TSource> source,
        int rowIndex,
        int columnIndex,
        params Func<TSource, object>[] propertySelectors);

    /// <summary>
    /// Returns the built <see cref="Table"/>.
    /// </summary>
    public Table Build();
}