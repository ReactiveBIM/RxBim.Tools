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
            string? transactionName = null)
        {
            var revitDocument = GetRevitDocument(transactionContext);
            var transaction = new Transaction(revitDocument, transactionName ?? GetUniqueTransactionName());
            return new RevitTransaction(transaction);
        }

        /// <inheritdoc />
        public ITransactionGroup CreateTransactionGroup(
            ITransactionContext? transactionContext = null,
            string? transactionGroupName = null)
        {
            var revitDocument = GetRevitDocument(transactionContext);
            var transactionGroup =
                new TransactionGroup(revitDocument, transactionGroupName ?? GetUniqueTransactionGroupName());
            return new RevitTransactionGroup(transactionGroup);
        }

        private string GetUniqueTransactionName()
        {
            return $"Transaction_{Guid.NewGuid()}";
        }

        private string GetUniqueTransactionGroupName()
        {
            return $"TransactionGroup_{Guid.NewGuid()}";
        }

        private Document GetRevitDocument(ITransactionContext? document)
        {
            return document is null
                ? _application.ActiveUIDocument.Document
                : document.ContextObject as Document ??
                  throw new ArgumentException("Must be a Revit document.", nameof(document.ContextObject));
        }
    }
}