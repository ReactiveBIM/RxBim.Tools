namespace RxBim.Tools.Autocad
{
    using JetBrains.Annotations;

    /// <summary>
    /// Service for <see cref="DatabaseContextWrapper"/>.
    /// </summary>
    [UsedImplicitly]
    internal class DatabaseContextService : ITransactionContextService<DatabaseContextWrapper>
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContextService"/> class.
        /// </summary>
        /// <param name="documentService"><see cref="IDocumentService"/> instance.</param>
        public DatabaseContextService(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <inheritdoc />
        public DatabaseContextWrapper GetDefaultContext()
        {
            return new DatabaseContextWrapper(_documentService.GetActiveDocument().Database);
        }
    }
}