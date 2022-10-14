namespace RxBim.Tools.Revit.Extensions;

using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

/// <summary>
/// Extensions for <see cref="FilteredElementCollector"/>.
/// </summary>
public static class FilteredElementCollectorExtensions
{
    /// <summary>
    /// Gets casted objects by quick filter.
    /// </summary>
    /// <param name="collector"><see cref="FilteredElementCollector"/></param>
    /// <typeparam name="T">Type to cast.</typeparam>
    public static IEnumerable<T> OfClass<T>(this FilteredElementCollector collector)
    {
        return collector
            .OfClass(typeof(T))
            .Cast<T>();
    }
}