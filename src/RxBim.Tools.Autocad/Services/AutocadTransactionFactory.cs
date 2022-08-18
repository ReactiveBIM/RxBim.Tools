namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
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
            var transactionManager = GetContextAndTransactionManager(transactionContext);
            return new AutocadTransaction(transactionManager.StartTransaction());
        }

        /// <inheritdoc />
        public ITransactionGroup CreateTransactionGroup(
            ITransactionContext? transactionContext = null,
            string? transactionGroupName = null)
        {
            var transactionManager = GetContextAndTransactionManager(transactionContext);
            return new AutocadTransactionGroup(transactionManager.StartTransaction());
        }

        private TransactionManager GetContextAndTransactionManager(ITransactionContext? transactionContext)
        {
            var context = transactionContext ?? _documentService.GetActiveDocument().ToTransactionContext();
            return context.GetTransactionManager();
        }
    }
}