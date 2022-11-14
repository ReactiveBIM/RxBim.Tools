namespace RxBim.Tools.Revit;

using System.Collections.Generic;

/// <summary>
/// Manager for <see cref="FilteredElementScope"/>.
/// </summary>
public interface IFilteredElementScopeManager
{
    /// <summary>
    /// <see cref="FilteredElementScope"/>
    /// </summary>
    /// <remarks>Default is <see cref="FilteredElementScope.AllModel"/>.</remarks>
    FilteredElementScope Scope { get; }
        
    /// <summary>
    /// Collection with <see cref="FilteredElementScope"/> for which include sub-elements.
    /// </summary>
    IReadOnlyCollection<FilteredElementScope> ScopesForIncludeSubElements { get; }

    /// <summary>
    /// Sets <see cref="IFilteredElementScopeManager.Scope"/>.
    /// </summary>
    /// <param name="scope"><see cref="FilteredElementScope"/></param>
    IElementsCollector SetScope(FilteredElementScope scope);

    /// <summary>
    /// Adds <see cref="FilteredElementScope"/> to <see cref="IFilteredElementScopeManager.ScopesForIncludeSubElements"/>.
    /// </summary>
    /// <param name="scope"><see cref="FilteredElementScope"/></param>
    /// <remarks>For another scopes not as <see cref="FilteredElementScope.SelectedElements"/>
    /// may performance problems when many elements.</remarks>
    IElementsCollector EnableScopeForIncludeSubElements(FilteredElementScope scope);
        
    /// <summary>
    /// Removes <see cref="FilteredElementScope"/> from <see cref="IFilteredElementScopeManager.ScopesForIncludeSubElements"/>.
    /// </summary>
    /// <param name="scope"><see cref="FilteredElementScope"/></param>
    IElementsCollector DisableScopeForIncludeSubElements(FilteredElementScope scope);
}