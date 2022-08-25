namespace RxBim.Tools.Autocad
{
    using Di;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class AutocadTransactionFactory : ITransactionFactory
    {
        private readonly IServiceLocator _locator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransactionFactory"/> class.
        /// </summary>
        /// <param name="documentService"><see cref="IDocumentService"/> instance.</param>
        /// <param name="locator"><see cref="IServiceLocator"/> instance.</param>
        public AutocadTransactionFactory(IDocumentService documentService, IServiceLocator locator)
        {
            _locator = locator;
        }

        /// <inheritdoc />
        public ITransaction CreateTransaction<T>(T context, string? name = null)
            where T : class, ITransactionContext
        {
            var acadTransaction = context.GetTransactionManager().StartTransaction();
            return new AutocadTransaction(acadTransaction);
        }

        /// <inheritdoc />
        public ITransactionGroup CreateTransactionGroup<T>(T context, string? name = null)
            where T : class, ITransactionContext
        {
            var acadTransaction = context.GetTransactionManager().StartTransaction();
            return new AutocadTransactionGroup(acadTransaction, context);
        }

        /// <inheritdoc />
        public T GetDefaultContext<T>()
            where T : class, ITransactionContext
        {
            var transactionContextFactory = _locator.GetService<ITransactionContextService<T>>();
            return transactionContextFactory.GetDefaultContext();
        }
    }
}