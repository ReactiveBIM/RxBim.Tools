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
        public (ITransaction Transaction, ITransactionContext Context) CreateTransaction(
            ITransactionContext? transactionContext = null,
            string? transactionName = null)
        {
            var (context, transactionManager) = GetContextAndTransactionManager(transactionContext);
            return (new AutocadTransaction(transactionManager.StartTransaction()), context);
        }

        /// <inheritdoc />
        public (ITransactionGroup Group, ITransactionContext Context) CreateTransactionGroup(
            ITransactionContext? transactionContext = null,
            string? transactionGroupName = null)
        {
            var (context, transactionManager) = GetContextAndTransactionManager(transactionContext);
            return (new AutocadTransactionGroup(transactionManager.StartTransaction()), context);
        }

        private (ITransactionContext Context, TransactionManager TransactionManager)
            GetContextAndTransactionManager(ITransactionContext? transactionContext)
        {
            var context = transactionContext ?? new AutocadTransactionContext(_documentService.GetActiveDocument());
            var transactionManager = context.GetTransactionManager();
            return (context, transactionManager);
        }
    }
}