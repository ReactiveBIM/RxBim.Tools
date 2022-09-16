namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.ApplicationServices;

    /// <inheritdoc cref="RxBim.Tools.Autocad.IDocumentWrapper" />
    public class DocumentWrapper : Wrapper<Document>, IDocumentWrapper
    {
        /// <inheritdoc />
        public DocumentWrapper(Document contextObject)
            : base(contextObject)
        {
        }
    }
}