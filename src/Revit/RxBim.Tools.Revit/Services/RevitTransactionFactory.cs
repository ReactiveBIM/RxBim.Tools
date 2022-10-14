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
        public ITransactionWrapper CreateTransaction<T>(T context, string? name = null)
            where T : class, ITransactionContextWrapper
        {
            var revitTransaction = new Transaction(context.GetDocument(), name ?? $"Transaction_{Guid.NewGuid()}");
            return new TransactionWrapper(revitTransaction);
        }

        /// <inheritdoc />
        public ITransactionGroupWrapper CreateTransactionGroup<T>(T context, string? name = null)
            where T : class, ITransactionContextWrapper
        {
            var transactionGroup =
                new TransactionGroup(context.GetDocument(), name ?? $"TransactionGroup_{Guid.NewGuid()}");
            return new TransactionGroupWrapper(transactionGroup);
        }

        /// <inheritdoc />
        public T GetDefaultContext<T>()
            where T : class, ITransactionContextWrapper
        {
            var service = _locator.GetService<ITransactionContextService<T>>();
            return service.GetDefaultContext();
        }
    }
}