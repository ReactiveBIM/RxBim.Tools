namespace RxBim.Tools.Autocad
{
    using Di;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    public class AutocadTransactionFactory : ITransactionFactory
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
        public ITransactionWrapper CreateTransaction<T>(T context, string? name = null)
            where T : class, ITransactionContextWrapper
        {
            var acadTransaction = context.GetTransactionManager().StartTransaction();
            return new TransactionWrapper(acadTransaction);
        }

        /// <inheritdoc />
        public ITransactionGroupWrapper CreateTransactionGroup<T>(T context, string? name = null)
            where T : class, ITransactionContextWrapper
        {
            var acadTransaction = context.GetTransactionManager().StartTransaction();
            return new TransactionGroupWrapper(acadTransaction);
        }

        /// <inheritdoc />
        public T GetDefaultContext<T>()
            where T : class, ITransactionContextWrapper
        {
            var contextService = _locator.GetService<ITransactionContextService<T>>();
            return contextService.GetDefaultContext();
        }
    }
}