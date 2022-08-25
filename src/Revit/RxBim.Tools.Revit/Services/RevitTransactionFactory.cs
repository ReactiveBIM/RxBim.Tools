namespace RxBim.Tools.Revit.Services
{
    using System;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;
    using Models;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class RevitTransactionFactory : ITransactionFactory
    {
        private readonly UIApplication _application;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevitTransactionFactory"/> class.
        /// </summary>
        /// <param name="application"><see cref="UIApplication"/> instance.</param>
        public RevitTransactionFactory(UIApplication application)
        {
            _application = application;
        }

        /// <inheritdoc />
        public ITransaction CreateTransaction(
            ITransactionContext? transactionContext = null,
            string? name = null)
        {
            var document = GetRevitDocument(transactionContext);
            var revitTransaction = new Transaction(document, name ?? GetUniqueTransactionName());
            return new RevitTransaction(revitTransaction, transactionContext ?? new TransactionContext(document));
        }

        /// <inheritdoc />
        public ITransactionGroup CreateTransactionGroup(
            ITransactionContext? transactionContext = null,
            string? name = null)
        {
            var revitDocument = GetRevitDocument(transactionContext);
            var transactionGroup =
                new TransactionGroup(revitDocument, name ?? GetUniqueTransactionGroupName());
            return new RevitTransactionGroup(transactionGroup,
                transactionContext ?? new TransactionContext(revitDocument));
        }

        private string GetUniqueTransactionName()
        {
            return $"Transaction_{Guid.NewGuid()}";
        }

        private string GetUniqueTransactionGroupName()
        {
            return $"TransactionGroup_{Guid.NewGuid()}";
        }

        private Document GetRevitDocument(ITransactionContext? context)
        {
            return context is null
                ? _application.ActiveUIDocument.Document
                : context.ContextObject as Document ??
                  throw new ArgumentException("Must be a Revit document.", nameof(context.ContextObject));
        }
    }
}