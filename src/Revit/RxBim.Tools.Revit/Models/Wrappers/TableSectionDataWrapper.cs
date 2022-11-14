namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="TableSectionData"/>.
/// </summary>
public class TableSectionDataWrapper
    : Wrapper<TableSectionData>, ITableSectionDataWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TableSectionDataWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="TableSectionData"/></param>
    public TableSectionDataWrapper(TableSectionData wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public int FirstRowNumber
        => Object.FirstRowNumber;

    /// <inheritdoc />
    public int LastRowNumber
        => Object.LastRowNumber;

    /// <inheritdoc />
    public string GetCellText(int rowIndex, int columnIndex)
        => Object.GetCellText(rowIndex, columnIndex);
}