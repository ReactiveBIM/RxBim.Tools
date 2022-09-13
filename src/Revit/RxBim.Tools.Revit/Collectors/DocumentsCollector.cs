namespace RxBim.Tools.Revit
{
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class DocumentsCollector : IDocumentsCollector
    {
        private readonly UIApplication _uiApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentsCollector"/> class.
        /// </summary>
        /// <param name="uiApplication"><see cref="UIApplication"/></param>
        public DocumentsCollector(UIApplication uiApplication)
        {
            _uiApplication = uiApplication;
        }
        
        /// <inheritdoc/>
        public IDocumentWrapper GetCurrentDocument()
        {
            return _uiApplication.ActiveUIDocument.Document.Wrap();
        }

        /// <inheritdoc/>
        public IEnumerable<IDocumentWrapper> GetAllDocuments(
            IDocumentWrapper? document = null)
        {
            document ??= GetCurrentDocument();
            var doc = document.Unwrap<Document>()!;
            var docWrappers = new FilteredElementCollector(doc)
                .OfClass(typeof(RevitLinkInstance))
                .Cast<RevitLinkInstance>()
                .Where(l => IsNotNestedLib(l, doc))
                .Select(l => l.GetLinkDocument())
                .Where(d => d != null)
                .Select(d => d.Wrap())
                .ToList();
            docWrappers.Insert(0, document);
            return docWrappers;
        }

        private bool IsNotNestedLib(RevitLinkInstance linkInstance, Document document)
        {
            var linkType = (RevitLinkType)document.GetElement(linkInstance.GetTypeId());
            
            return linkType.GetLinkedFileStatus() == LinkedFileStatus.Loaded
                   && !linkType.IsNestedLink;
        }
    }
}
