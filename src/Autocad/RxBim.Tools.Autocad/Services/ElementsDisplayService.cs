namespace RxBim.Tools.Autocad;

using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;

/// <inheritdoc />
internal class ElementsDisplayService(IDocumentService documentService) : IElementsDisplay
{
    /// <inheritdoc />
    public void SetSelectedElements(IEnumerable<IObjectIdWrapper> ids)
    {
        var activeDocument = documentService.GetActiveDocument();
        var activeDocumentDb = activeDocument.Database;
        var activeDocIds = ids
            .Select(x => x.Unwrap<ObjectId>())
            .Where(x => x.Database.Equals(activeDocumentDb))
            .ToArray();
        if (!activeDocIds.Any())
            return;

        using var lockDocument = activeDocument.LockDocument();
        activeDocument.Editor.SetImpliedSelection(activeDocIds);
    }

    /// <inheritdoc />
    public void SetSelectedElement(IObjectIdWrapper id) => SetSelectedElements([id]);

    /// <inheritdoc />
    public void ResetSelection() => documentService.GetActiveDocument().Editor.SetImpliedSelection([]);

    /// <inheritdoc />
    public void Zoom(IObjectIdWrapper id, double zoomFactor = 0.25)
    {
        var objId = id.Unwrap<ObjectId>();
        var activeDocument = documentService.GetActiveDocument();
        if (!objId.Database.Equals(activeDocument.Database))
            return;

        using var lockDocument = activeDocument.LockDocument();

        try
        {
            using var ent = objId.OpenAs<Entity>();
            activeDocument.Editor.Zoom(ent.GeometricExtents.Zoom(zoomFactor));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}