namespace RxBim.Tools.Revit.Services
{
    using System;
    using Autodesk.Revit.DB;
    using Di;
    using Extensions;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class RevitTransactionFactory : ITransactionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransactionFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> instance.</param>
        public RevitTransactionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
            var service = _serviceProvider.GetService<ITransactionContextService<T>>();
            return service.GetDefaultContext();
        }
    }
}