namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;
    using TransactionManager = Autodesk.AutoCAD.DatabaseServices.TransactionManager;

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
        public ITransaction GetTransaction(object? transactionContext = null, string? transactionName = null)
        {
            var transactionManager = GetTransactionManager(transactionContext);
            return new AutocadTransaction(transactionManager.StartTransaction());
        }

        /// <inheritdoc />
        public ITransactionGroup GetTransactionGroup(object? document = null, string? transactionGroupName = null)
        {
            var transactionManager = GetTransactionManager(document);
            return new AutocadTransactionGroup(transactionManager.StartTransaction());
        }

        private TransactionManager GetTransactionManager(object? document)
        {
            return document switch
            {
                null => _documentService.GetActiveDocument().TransactionManager,
                Document acadDocument => acadDocument.TransactionManager,
                Database database => database.TransactionManager,
                _ => throw new ArgumentException(
                    $"Incorrect type: {document.GetType().Name}. Must be a Revit document.",
                    nameof(document))
            };
        }
    }
}