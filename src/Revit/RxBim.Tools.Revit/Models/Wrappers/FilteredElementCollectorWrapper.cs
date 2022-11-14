namespace RxBim.Tools.Revit;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="FilteredElementCollector"/>.
/// </summary>
public class FilteredElementCollectorWrapper
    : Wrapper<FilteredElementCollector>, IFilteredElementCollectorWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FilteredElementCollectorWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="FilteredElementCollector"/></param>
    public FilteredElementCollectorWrapper(FilteredElementCollector wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public IEnumerator<IElementWrapper> GetEnumerator()
    {
        using var baseEnum = Object.GetEnumerator();
        while (baseEnum.MoveNext())
            yield return baseEnum.Current!.Wrap();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <inheritdoc />
    public IFilteredElementCollectorWrapper OfCategory(BuiltInCategory category)
    {
        // Filters accumulate into FilteredElementCollector
        Object.OfCategory(category);
        return this;
    }

    /// <inheritdoc />
    public IFilteredElementCollectorWrapper OfClass<T>()
        where T : IWrapper
    {
        var castType = typeof(T);
        var baseClassType = castType.GetWrapperBaseType() ?? Assembly
            .GetCallingAssembly()
            .GetTypes()
            .Where(t => !t.IsInterface && !t.IsAbstract)
            .FirstOrDefault(t => castType.IsInterface
                ? t.GetInterfaces().Contains(castType)
                : t.GetBaseClass(bt => bt == castType) is not null)
            ?.GetWrapperBaseType();

        var objectType = baseClassType
            ?.GetGenericArguments()
            .First();
        
        // Filters accumulate into FilteredElementCollector
        Object.OfClass(objectType);
        return this;
    }

    /// <inheritdoc />
    public IFilteredElementCollectorWrapper WherePasses(IElementFilterWrapper filter)
    {
        var filterType = filter.GetType();
        var filterObjectType = filterType
            .GetWrapperBaseType()
            ?.GetGenericArguments()
            .First();
        var unwrapMethod = filterType
            .GetMethod(nameof(IWrapper.Unwrap))
            ?.MakeGenericMethod(filterObjectType);
        var filterObject = unwrapMethod?.Invoke(filter, null) as ElementFilter;
        
        // Filters accumulate into FilteredElementCollector
        Object.WherePasses(filterObject);
        return this;
    }

    /// <inheritdoc />
    public IFilteredElementCollectorWrapper WhereElementIsElementType()
    {
        // Filters accumulate into FilteredElementCollector
        Object.WhereElementIsElementType();
        return this;
    }

    /// <inheritdoc />
    public IFilteredElementCollectorWrapper WhereElementIsNotElementType()
    {
        // Filters accumulate into FilteredElementCollector
        Object.WhereElementIsNotElementType();
        return this;
    }

    /// <inheritdoc />
    public int GetElementCount()
        => Object.GetElementCount();

    /// <inheritdoc />
    public IList<IElementWrapper> ToElements()
        => Object.ToElements()
            .Select(element => element.Wrap())
            .ToList();
}