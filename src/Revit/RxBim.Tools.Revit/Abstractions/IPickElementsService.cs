namespace RxBim.Tools.Revit;

using System;
using System.Collections.Generic;

/// <summary>
/// Service for pick elements on model.
/// </summary>
public interface IPickElementsService
{
    /// <summary>
    /// Picks <see cref="IElementWrapper"/> on model.
    /// </summary>
    /// <param name="filterElement">Filter <see cref="IElementWrapper"/>.</param>
    /// <param name="statusPrompt">Status description.</param>
    /// <param name="saveSelectedElement">If true,
    /// saves selected element for returns via <see cref="IElementsCollector"/>
    /// with <see cref="FilteredElementScope.SelectedElements"/>.</param>
    /// <remarks>If cancel selection returns null.</remarks>
    IElementWrapper? PickElement(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "",
        bool saveSelectedElement = true);

    /// <summary>
    /// Picks some <see cref="IElementWrapper"/> on model.
    /// </summary>
    /// <param name="filterElement">Filter <see cref="IElementWrapper"/>.</param>
    /// <param name="statusPrompt">Status description.</param>
    /// <param name="saveSelectedElements">If true,
    /// saves selected elements for returns via <see cref="IElementsCollector"/>
    /// with <see cref="FilteredElementScope.SelectedElements"/>.</param>
    /// <remarks>If cancel selection returns null.</remarks>
    IEnumerable<IElementWrapper>? PickElements(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "",
        bool saveSelectedElements = true);

    /// <summary>
    /// Picks linked <see cref="IElementWrapper"/> on model.
    /// </summary>
    /// <param name="filterElement">Filter <see cref="IElementWrapper"/>.</param>
    /// <param name="statusPrompt">Status description.</param>
    /// <remarks>If cancel selection returns null.</remarks>
    (IElementWrapper LinkedElement, IRevitLinkInstanceWrapper LinkInstance)? PickLinkedElement(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "");

    /// <summary>
    /// Picks some linked <see cref="IElementWrapper"/> on model.
    /// </summary>
    /// <param name="filterElement">Filter <see cref="IElementWrapper"/>.</param>
    /// <param name="statusPrompt">Status description.</param>
    /// <remarks>If cancel selection returns null.</remarks>
    IEnumerable<(IElementWrapper LinkedElement, IRevitLinkInstanceWrapper LinkInstance)>? PickLinkedElements(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "");
}