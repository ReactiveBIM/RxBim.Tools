namespace RxBim.Tools.Revit;

using System.Collections.Generic;
using Autodesk.Revit.DB;
using JetBrains.Annotations;

/// <summary>
/// Wrapper for FilteredElementCollector type.
/// </summary>
[PublicAPI]
public interface IFilteredElementCollectorWrapper : IWrapper, IEnumerable<IElementWrapper>
{
    /// <summary>
    /// Applies on filter by <see cref="BuiltInCategory"/> to the collector.
    /// </summary>
    /// <param name="category"><see cref="BuiltInCategory"/></param>
    IFilteredElementCollectorWrapper OfCategory(BuiltInCategory category);
    
    /// <summary>
    /// Applies an filter by <see cref="IWrapper"/> type to the collector.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IWrapper"/>.</typeparam>
    IFilteredElementCollectorWrapper OfClass<T>()
        where T : IWrapper;

    /// <summary>
    /// Applies an <see cref="IElementFilterWrapper"/> to the collector.
    /// </summary>
    /// <param name="filter"><see cref="IElementFilterWrapper"/></param>
    IFilteredElementCollectorWrapper WherePasses(IElementFilterWrapper filter);

    /// <summary>
    /// Applies an ElementIsElementTypeFilter to the collector.
    /// </summary>
    IFilteredElementCollectorWrapper WhereElementIsElementType();

    /// <summary>
    /// Applies an inverted ElementIsElementTypeFilter to the collector.
    /// </summary>
    IFilteredElementCollectorWrapper WhereElementIsNotElementType();

    /// <summary>
    /// Gets the number of elements in your current filter.
    /// </summary>
    public int GetElementCount();

    /// <summary>
    /// Returns the complete set of elements that pass the filter(s).
    /// </summary>
    public IList<IElementWrapper> ToElements();
}