namespace RxBim.Tools.Revit
{
    using System.Collections.Generic;

    /// <summary>
    /// Collector of <see cref="IDocumentWrapper"/>.
    /// </summary>
    public interface IDocumentsCollector
    {
        /// <summary>
        /// Gets current opened <see cref="IDocumentWrapper"/>.
        /// </summary>
        IDocumentWrapper GetCurrentDocument();

        /// <summary>
        /// Gets all <see cref="IDocumentWrapper"/> 
        /// </summary>
        /// <param name="document">Main <see cref="IDocumentWrapper"/>.
        /// If null, documents takes from current document.</param>
        /// <remarks>Returns <see cref="IDocumentWrapper"/> from linked documents and insert main document.</remarks>
        IEnumerable<IDocumentWrapper> GetAllDocuments(IDocumentWrapper? document = null);
    }
}
