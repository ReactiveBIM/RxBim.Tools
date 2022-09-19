namespace RxBim.Tools.Revit;

/// <summary>
/// Wrapper for TableSectionData type.
/// </summary>
public interface ITableSectionDataWrapper : IWrapper
{
    /// <summary>
    /// The first row in this section of the table.
    /// </summary>
    int FirstRowNumber { get; }
    
    /// <summary>
    /// The last row in this section of the table.
    /// </summary>
    int LastRowNumber { get; }

    /// <summary>
    /// Gets the text shown by this cell, if the cell's type is CellType.Text or CellType.ParameterText.
    /// </summary>
    /// <param name="rowIndex">The cell row.</param>
    /// <param name="columnIndex">The cell column.</param>
    /// <remarks>The text in the cell, or an empty string if the type if not CellType.Text or CellType.ParameterText.</remarks>
    string GetCellText(int rowIndex, int columnIndex);
}