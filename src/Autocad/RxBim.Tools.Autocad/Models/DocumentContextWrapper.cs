namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.ApplicationServices;

    /// <inheritdoc />
    public class DocumentContextWrapper : TransactionContextWrapper<Document>
    {
        /// <inheritdoc />
        public DocumentContextWrapper(Document contextObject)
            : base(contextObject)
        {
        }
    }
}