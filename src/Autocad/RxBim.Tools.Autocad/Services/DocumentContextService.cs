namespace RxBim.Tools.Autocad
{
    using JetBrains.Annotations;

    /// <summary>
    /// Service for <see cref="DocumentWrapper"/>.
    /// </summary>
    [UsedImplicitly]
    internal class DocumentContextService : ITransactionContextService<DocumentWrapper>
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentContextService"/> class.
        /// </summary>
        /// <param name="documentService"><see cref="IDocumentService"/> instance.</param>
        public DocumentContextService(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <inheritdoc />
        public DocumentWrapper GetDefaultContext()
        {
            return new DocumentWrapper(_documentService.GetActiveDocument());
        }
    }
}