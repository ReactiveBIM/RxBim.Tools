namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class TransactionService : ITransactionService
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="documentService"><see cref="IDocumentService"/> instance.</param>
        public TransactionService(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <inheritdoc />
        public void RunInTransaction(Action action, Database? database = null)
        {
            database = GetDatabase(database);
            using var transaction = database.TransactionManager.StartTransaction();
            action();
            transaction.Commit();
        }

        /// <inheritdoc />
        public T RunInTransaction<T>(Func<T> func, Database? database = null)
        {
            database = GetDatabase(database);
            using var transaction = database.TransactionManager.StartTransaction();
            var result = func();
            transaction.Commit();
            return result;
        }

        /// <inheritdoc />
        public void RunInTransaction(Action<Transaction> action, Database? database = null)
        {
            database = GetDatabase(database);
            using var transaction = database.TransactionManager.StartTransaction();
            action(transaction);
            transaction.Commit();
        }

        /// <inheritdoc />
        public T RunInTransaction<T>(Func<Transaction, T> func, Database? database = null)
        {
            database = GetDatabase(database);
            using var transaction = database.TransactionManager.StartTransaction();
            var result = func(transaction);
            transaction.Commit();
            return result;
        }

        private Database GetDatabase(Database? database)
        {
            database ??= _documentService.GetActiveDocument().Database;
            return database;
        }
    }
}