namespace RxBim.Tools.Autocad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc />
    internal class ElementsDisplayService : IElementsDisplay
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementsDisplayService"/> class.
        /// </summary>
        /// <param name="documentService"><see cref="IDocumentService"/> instance.</param>
        public ElementsDisplayService(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <inheritdoc />
        public void SetSelectedElements(IEnumerable<IIdentifierWrapper> ids)
        {
            var activeDocument = _documentService.GetActiveDocument();
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
        public void SetSelectedElement(IIdentifierWrapper id)
        {
            SetSelectedElements(new[] { id });
        }

        /// <inheritdoc />
        public void ResetSelection()
        {
            _documentService.GetActiveDocument().Editor.SetImpliedSelection(Array.Empty<ObjectId>());
        }

        /// <inheritdoc />
        public void Zoom(IIdentifierWrapper id, double zoomFactor = 0.25)
        {
            var objId = id.Unwrap<ObjectId>();
            var activeDocument = _documentService.GetActiveDocument();
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
}