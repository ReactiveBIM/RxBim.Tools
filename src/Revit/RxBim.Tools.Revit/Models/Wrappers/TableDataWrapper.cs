namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="TableData"/>.
/// </summary>
public class TableDataWrapper
    : Wrapper<TableData>, ITableDataWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TableDataWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="TableData"/></param>
    public TableDataWrapper(TableData wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public ITableSectionDataWrapper? GetSectionData(int index)
        => Object.GetSectionData(index)?.Wrap();

    /// <inheritdoc />
    public ITableSectionDataWrapper? GetSectionData(SectionType sectionType)
        => Object.GetSectionData(sectionType)?.Wrap();
}