namespace RxBim.Tools.Revit.Models
{
    using Autodesk.Revit.DB;

    /// <summary>
    /// The transaction context for <see cref="Document"/>.
    /// </summary>
    public class DocumentContextWrapper : TransactionContextWrapper<Document>
    {
        /// <inheritdoc />
        public DocumentContextWrapper(Document contextObject)
            : base(contextObject)
        {
        }
    }
}