namespace RxBim.Tools.Autocad
{
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class AutocadTransactionFactory : ITransactionFactory
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransactionFactory"/> class.
        /// </summary>
        /// <param name="documentService"><see cref="IDocumentService"/> instance.</param>
        public AutocadTransactionFactory(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <inheritdoc />
        public ITransaction CreateTransaction(
            ITransactionContext? transactionContext = null,
            string? transactionName = null)
        {
            var context = GetContext(transactionContext);
            var acadTransaction = context.ToDatabase().TransactionManager.StartTransaction();
            return new AutocadTransaction(acadTransaction, context);
        }

        /// <inheritdoc />
        public ITransactionGroup CreateTransactionGroup(
            ITransactionContext? transactionContext = null,
            string? transactionGroupName = null)
        {
            var context = GetContext(transactionContext);
            var acadTransaction = context.ToDatabase().TransactionManager.StartTransaction();
            return new AutocadTransactionGroup(acadTransaction, context);
        }

        private ITransactionContext GetContext(ITransactionContext? transactionContext)
        {
            return transactionContext ?? _documentService.GetActiveDocument().ToTransactionContext();
        }
    }
}