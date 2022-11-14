namespace RxBim.Tools.Revit;

using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JetBrains.Annotations;

/// <inheritdoc />
[UsedImplicitly]
public class ElementsCollector : IElementsCollector
{
    private readonly UIApplication _uiApplication;
    private readonly IElementsDisplay _elementsDisplay;
    
    private readonly HashSet<FilteredElementScope> _scopesForIncludeSubElements = new();
    private readonly Dictionary<string, List<ElementId>> _selectedElementsIds = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementsCollector"/> class.
    /// </summary>
    /// <param name="uiApplication"><see cref="UIApplication"/></param>
    /// <param name="elementsDisplay"><see cref="IElementsDisplay"/></param>
    public ElementsCollector(
        UIApplication uiApplication,
        IElementsDisplay elementsDisplay)
    {
        _uiApplication = uiApplication;
        _elementsDisplay = elementsDisplay;
    }

    /// <inheritdoc />
    public FilteredElementScope Scope { get; private set; } = FilteredElementScope.AllModel;

    /// <inheritdoc />
    public IReadOnlyCollection<FilteredElementScope> ScopesForIncludeSubElements
        => _scopesForIncludeSubElements;

    private UIDocument UiDocument => _uiApplication.ActiveUIDocument;
    private Document Document => _uiApplication.ActiveUIDocument.Document;

    /// <inheritdoc />
    public IElementsCollector SetScope(
        FilteredElementScope scope)
    {
        Scope = scope;
        return this;
    }

    /// <inheritdoc />
    public IElementsCollector EnableScopeForIncludeSubElements(
        FilteredElementScope scope)
    {
        _scopesForIncludeSubElements.Add(scope);
        return this;
    }

    /// <inheritdoc />
    public IElementsCollector DisableScopeForIncludeSubElements(
        FilteredElementScope scope)
    {
        _scopesForIncludeSubElements.Remove(scope);
        return this;
    }

    /// <inheritdoc />
    public IElementsCollector SaveElements(IEnumerable<IObjectIdWrapper>? selectedElementsIds = null)
    {
        var selectedIds = selectedElementsIds
            ?.Select(elem => elem.Unwrap<ElementId>())
                          ?? UiDocument.Selection.GetElementIds();

        SaveSelectedElementsIds(selectedIds.ToList());

        return this;
    }

    /// <inheritdoc />
    public IElementsCollector SaveAndResetSelectedElements()
    {
        var selectedIds = UiDocument.Selection.GetElementIds();
        
        SaveSelectedElementsIds(selectedIds.ToList());

        _elementsDisplay.ResetSelection();

        return this;
    }

    /// <inheritdoc />
    public IElementsCollector SetBackSavedSelectedElements()
    {
        if (_selectedElementsIds.ContainsKey(Document.Title))
        {
            _elementsDisplay.SetSelectedElements(_selectedElementsIds[Document.Title]
                .Select(e => e.Wrap())
                .ToList());
        }

        return this;
    }

    /// <inheritdoc />
    public IElementsCollector ResetSavedSelectedElements()
    {
        _selectedElementsIds.Remove(Document.Title);
        return this;
    }

    /// <inheritdoc />
    public IFilteredElementCollectorWrapper GetCollector(
        IDocumentWrapper? document = null, FilteredElementScope? scope = null)
    {
        var doc = document?.Unwrap<Document>() ?? Document;
        
        // Saves and resets selected elements on model for not blocking Revit context.
        SaveAndResetSelectedElements();

        scope ??= Scope;

        switch (scope)
        {
            case FilteredElementScope.AllModel:
            {
                var collector = new FilteredElementCollector(doc); 
                return !_scopesForIncludeSubElements.Contains(scope.Value)
                    ? collector.Wrap()
                    : GetCollectorWrapperWithSubElements(collector, doc);
            }
            
            case FilteredElementScope.ActiveView:
            {
                var collector = new FilteredElementCollector(doc, UiDocument.ActiveGraphicalView.Id);
                return !_scopesForIncludeSubElements.Contains(scope.Value)
                    ? collector.Wrap()
                    : GetCollectorWrapperWithSubElements(collector, doc);
            }

            case FilteredElementScope.SelectedElements:
            {
                if (!_selectedElementsIds.ContainsKey(doc.Title)
                    || !_selectedElementsIds[doc.Title].Any())
                    return new FilteredElementCollector(doc, new List<ElementId> { ElementId.InvalidElementId }).Wrap();

                var selectedIds = _selectedElementsIds[doc.Title];

                if (!_scopesForIncludeSubElements.Contains(scope.Value))
                    return new FilteredElementCollector(doc, selectedIds).Wrap();
                
                selectedIds.AddRange(selectedIds.SelectMany(elemId => GetSubElements(elemId, doc)));
                return new FilteredElementCollector(doc, selectedIds).Wrap();
            }

            default:
                throw new NotImplementedException($"Not implemented {scope} scope.");
        }
    }

    private void SaveSelectedElementsIds(List<ElementId> elementsIds)
    {
        if (!elementsIds.Any())
            return;

        if (_selectedElementsIds.ContainsKey(Document.Title))
            _selectedElementsIds[Document.Title] = elementsIds;
        else
            _selectedElementsIds.Add(Document.Title, elementsIds);
    }

    private IFilteredElementCollectorWrapper GetCollectorWrapperWithSubElements(
        FilteredElementCollector collector, Document doc)
    {
        var elementsIds = collector
            .ToElementIds()
            .ToList();
        elementsIds.AddRange(elementsIds
            .SelectMany(elemId => GetSubElements(elemId, doc)));
        return new FilteredElementCollector(doc, elementsIds).Wrap();
    }
    
    private IEnumerable<ElementId> GetSubElements(ElementId elementId, Document document)
    {
        switch (document.GetElement(elementId))
        {
            case FamilyInstance familyInstance:
            {
                var subFamilyIds = familyInstance.GetSubComponentIds();
                if (subFamilyIds == null)
                    yield break;
                
                foreach (var subFamilyId in subFamilyIds)
                {
                    if (document.GetElement(subFamilyId) is not FamilyInstance)
                        continue;
                    
                    yield return subFamilyId;

                    foreach (var family in GetSubElements(subFamilyId, document))
                        yield return family;
                }

                break;
            }

            case Group group:
            {
                foreach (var memberId in group.GetMemberIds())
                    yield return memberId;

                break;
            }

            case AssemblyInstance assembly:
            {
                foreach (var memberId in assembly.GetMemberIds())
                    yield return memberId;

                break;
            }

            case Wall { IsStackedWall: true } wall:
            {
                foreach (var panelId in wall.GetDependentElements(new ElementClassFilter(typeof(Panel))))
                    yield return panelId;

                break;
            }

            default:
                yield break;
        }
    }
}