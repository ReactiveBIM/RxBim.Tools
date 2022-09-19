namespace RxBim.Tools.Revit;

using Autodesk.Revit.DB;

/// <summary>
/// Wrapper for TableData type.
/// </summary>
public interface ITableDataWrapper : IWrapper
{
    /// <summary>
    /// Gets the section data array element at the specified index.
    /// </summary>
    /// <param name="index">The index of section data array. If the index is out of the boundary of section data array,
    /// <see langword="null" /> is returned.</param>
    ITableSectionDataWrapper? GetSectionData(int index);

    /// <summary>
    /// Gets the pointer to the section data array element at the specified section type.
    /// </summary>
    /// <param name="sectionType">The section type of section data array.
    /// If the integral value of the section type is out of the boundary of section data array,
    /// <see langword="null" /> is returned.</param>
    ITableSectionDataWrapper? GetSectionData(SectionType sectionType);
}