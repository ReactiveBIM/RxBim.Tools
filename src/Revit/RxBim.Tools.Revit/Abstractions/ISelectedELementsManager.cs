namespace RxBim.Tools.Revit;

using System.Collections.Generic;

/// <summary>
/// Manager for selected elements.
/// </summary>
public interface ISelectedElementsManager
{
    /// <summary>
    /// Saves <see cref="IElementWrapper"/> for returns via <see cref="IElementsCollector"/>
    /// with <see cref="FilteredElementScope.SelectedElements"/>.
    /// </summary>
    /// <param name="selectedElements">Savable <see cref="IElementWrapper"/> from current document.</param>
    /// <remarks>Saving only for current document.</remarks>
    IElementsCollector SaveElements(IEnumerable<IElementWrapper>? selectedElements = null);

    /// <summary>
    /// Saves selected elements for returns via <see cref="IElementsCollector"/>
    /// with <see cref="FilteredElementScope.SelectedElements"/> and then resets theirs. 
    /// </summary>
    /// <remarks>Saving and resetting only for current document.</remarks>
    IElementsCollector SaveAndResetSelectedElements();

    /// <summary>
    /// Sets saved selected elements and then resets theirs.
    /// </summary>
    /// <remarks>Resetting only for current document.</remarks>
    IElementsCollector SetBackSavedSelectedElements();

    /// <summary>
    /// Resets saved selected elements.
    /// </summary>
    /// <remarks>Resetting only for current document.</remarks>
    IElementsCollector ResetSavedSelectedElements();
}