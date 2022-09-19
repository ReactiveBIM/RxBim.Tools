namespace RxBim.Tools.Revit.Models
{
    using Abstractions;
    using Autodesk.Revit.DB;

    /// <inheritdoc cref="IDocumentWrapper" />
    public class DocumentWrapper : Wrapper<Document>, IDocumentWrapper
    {
        /// <inheritdoc />
        public DocumentWrapper(Document contextObject)
            : base(contextObject)
        {
        }
    }
}