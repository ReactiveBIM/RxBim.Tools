namespace RxBim.Tools.Revit.Services
{
    using System;
    using Autodesk.Revit.DB;
    using Di;
    using Extensions;
    using JetBrains.Annotations;
    using Models;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class RevitTransactionFactory : ITransactionFactory
    {
        private readonly IServiceLocator _locator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransactionFactory"/> class.
        /// </summary>
        /// <param name="locator"><see cref="IServiceLocator"/> instance.</param>
        public RevitTransactionFactory(IServiceLocator locator)
        {
            _locator = locator;
        }

        /// <inheritdoc />
        public ITransaction CreateTransaction<T>(T context, string? name = null)
            where T : class, ITransactionContext
        {
            var revitTransaction = new Transaction(context.GetDocument(), name ?? $"Transaction_{Guid.NewGuid()}");
            return new RevitTransaction(revitTransaction);
        }

        /// <inheritdoc />
        public ITransactionGroup CreateTransactionGroup<T>(T context, string? name = null)
            where T : class, ITransactionContext
        {
            var transactionGroup =
                new TransactionGroup(context.GetDocument(), name ?? $"TransactionGroup_{Guid.NewGuid()}");
            return new RevitTransactionGroup(transactionGroup);
        }

        /// <inheritdoc />
        public T GetDefaultContext<T>()
            where T : class, ITransactionContext
        {
            var service = _locator.GetService<ITransactionContextService<T>>();
            return service.GetDefaultContext();
        }
    }
}