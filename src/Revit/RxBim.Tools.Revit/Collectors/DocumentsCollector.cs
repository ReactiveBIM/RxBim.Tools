﻿namespace RxBim.Tools.Revit.Collectors;

using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JetBrains.Annotations;

/// <summary>
/// Коллектор документов
/// </summary>
[UsedImplicitly]
internal class DocumentsCollector : IDocumentsCollector
{
    private readonly UIApplication _uiApplication;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="uiApplication">Current <see cref="UIApplication"/></param>
    public DocumentsCollector(UIApplication uiApplication)
    {
        _uiApplication = uiApplication;
    }

    /// <inheritdoc/>
    public IEnumerable<string> GetDocumentsTitles()
    {
        var doc = _uiApplication.ActiveUIDocument.Document;
        var titles = new FilteredElementCollector(doc)
            .OfClass(typeof(RevitLinkInstance))
            .Cast<RevitLinkInstance>()
            .Where(l => IsNotNestedLib(l))
            .Select(l => l.GetLinkDocument())
            .Where(d => d != null)
            .Select(d => d.Title)
            .ToList();
        titles.Insert(0, doc.Title);

        return titles;
    }

    /// <inheritdoc/>
    public string GetMainDocumentTitle()
    {
        return _uiApplication.ActiveUIDocument.Document.Title;
    }

    private bool IsNotNestedLib(RevitLinkInstance linkInstance)
    {
        var linkType = (RevitLinkType)_uiApplication.ActiveUIDocument.Document
            .GetElement(linkInstance.GetTypeId());

        return linkType.GetLinkedFileStatus() == LinkedFileStatus.Loaded
               && !linkType.IsNestedLink;
    }
}