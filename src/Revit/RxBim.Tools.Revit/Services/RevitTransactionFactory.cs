namespace RxBim.Tools.Revit.Services
{
    using System;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
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
        /// <param name="application"><see cref="UIApplication"/> instance.</param>
        /// <param name="locator"><see cref="IServiceLocator"/> instance.</param>
        public RevitTransactionFactory(UIApplication application, IServiceLocator locator)
        {
            _locator = locator;
        }

        /// <inheritdoc />
        public ITransaction CreateTransaction<T>(T context, string? name = null)
            where T : class, ITransactionContext
        {
            var revitTransaction = new Transaction(context.GetDocument(), name ?? GetUniqueTransactionName());
            return new RevitTransaction(revitTransaction);
        }

        /// <inheritdoc />
        public ITransactionGroup CreateTransactionGroup<T>(T context, string? name = null)
            where T : class, ITransactionContext
        {
            var transactionGroup =
                new TransactionGroup(context.GetDocument(), name ?? GetUniqueTransactionGroupName());
            return new RevitTransactionGroup(transactionGroup);
        }

        /// <inheritdoc />
        public T GetDefaultContext<T>()
            where T : class, ITransactionContext
        {
            var service = _locator.GetService<ITransactionContextService<T>>();
            return service.GetDefaultContext();
        }

        private string GetUniqueTransactionName()
        {
            return $"Transaction_{Guid.NewGuid()}";
        }

        private string GetUniqueTransactionGroupName()
        {
            return $"TransactionGroup_{Guid.NewGuid()}";
        }
    }
}