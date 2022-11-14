namespace RxBim.Tools.Revit;

using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using JetBrains.Annotations;

/// <inheritdoc />
[UsedImplicitly]
public class PickElementsService : IPickElementsService
{
    private readonly UIApplication _uiApplication;
    private readonly IElementsCollector _elementsCollector;
    private readonly IElementsDisplay _elementsDisplay;

    /// <summary>
    /// Initializes a new instance of the <see cref="PickElementsService"/> class.
    /// </summary>
    /// <param name="uiApplication"><see cref="UIApplication"/></param>
    /// <param name="elementsCollector"><see cref="IElementsCollector"/></param>
    /// <param name="elementsDisplay"><see cref="IElementsDisplay"/></param>
    public PickElementsService(
        UIApplication uiApplication,
        IElementsCollector elementsCollector,
        IElementsDisplay elementsDisplay)
    {
        _uiApplication = uiApplication;
        _elementsCollector = elementsCollector;
        _elementsDisplay = elementsDisplay;
    }

    private UIDocument UiDocument => _uiApplication.ActiveUIDocument;

    private Document Document => _uiApplication.ActiveUIDocument.Document;
    
    /// <inheritdoc />
    public IElementWrapper? PickElement(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "",
        bool saveSelectedElement = true)
    {
        try
        {
            var pickRef = UiDocument.Selection.PickObject(
                ObjectType.Element,
                new ElementSelectionFilter(GetPredicate(filterElement)),
                statusPrompt);

            var pickElement = Document.GetElement(pickRef.ElementId).Wrap();
            if (saveSelectedElement)
                _elementsCollector.SaveElements(new[] { pickElement.Id });

            return pickElement;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public IEnumerable<IElementWrapper>? PickElements(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "",
        bool saveSelectedElements = true)
    {
        try
        {
            var pickElements = UiDocument.Selection.PickObjects(
                    ObjectType.Element,
                    new ElementSelectionFilter(GetPredicate(filterElement)),
                    statusPrompt)
                .Select(r => Document.GetElement(r))
                .Select(elem => elem.Wrap())
                .ToList();

            if (saveSelectedElements)
                _elementsCollector.SaveElements(pickElements.Select(elem => elem.Id));

            return pickElements;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public (IElementWrapper LinkedElement, IRevitLinkInstanceWrapper LinkInstance)? PickLinkedElement(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "")
    {
        try
        {
            var pickRef = UiDocument.Selection.PickObject(
                ObjectType.LinkedElement,
                new LinkedElementSelectionFilter(Document, GetPredicate(filterElement)),
                statusPrompt);

            // You can't save linked element to IElementCollector, because Revit API can't add it to UIDocument.Selection.
            _elementsDisplay.ResetSelection();

            return (Document.GetElement(pickRef.LinkedElementId).Wrap(),
                ((RevitLinkInstance)Document.GetElement(pickRef)).Wrap());
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public IEnumerable<(IElementWrapper LinkedElement, IRevitLinkInstanceWrapper LinkInstance)>? PickLinkedElements(
        Predicate<IElementWrapper>? filterElement = null,
        string statusPrompt = "")
    {
        try
        {
            var pickElements = UiDocument.Selection.PickObjects(
                    ObjectType.LinkedElement,
                    new LinkedElementSelectionFilter(Document, GetPredicate(filterElement)),
                    statusPrompt)
                .Select(pickRef => (Document.GetElement(pickRef.LinkedElementId).Wrap(),
                    ((RevitLinkInstance)Document.GetElement(pickRef)).Wrap()))
                .ToList();

            // You can't save linked element to IElementCollector, because Revit API can't add it to UIDocument.Selection.
            _elementsDisplay.ResetSelection();

            return pickElements;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    private Predicate<Element>? GetPredicate(Predicate<IElementWrapper>? predicate)
    {
        return predicate is not null
            ? elem => predicate.Invoke(elem.Wrap())
            : null;
    }
}