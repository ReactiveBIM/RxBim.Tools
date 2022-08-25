namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.ApplicationServices;

    /// <inheritdoc />
    public class DocumentContext : TransactionContext<Document>
    {
        /// <inheritdoc />
        public DocumentContext(Document contextObject)
            : base(contextObject)
        {
        }
    }
}