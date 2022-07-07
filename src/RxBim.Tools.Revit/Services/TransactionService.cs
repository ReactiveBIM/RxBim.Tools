namespace RxBim.Tools.Revit.Services
{
    using System;
    using Abstractions;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;

    /// <inheritdoc/>
    [UsedImplicitly]
    internal class TransactionService : ITransactionService
    {
        private readonly UIApplication _uiApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="uiApplication"><see cref="UIApplication"/></param>
        public TransactionService(UIApplication uiApplication)
        {
            _uiApplication = uiApplication;
        }

        private Document CurrentDocument => _uiApplication.ActiveUIDocument.Document;

        /// <inheritdoc/>
        public void RunInTransaction(Action action, string transactionName, Document? document = null)
        {
            RunInTransaction(action.ConvertToFunc(), transactionName, document);
        }

        /// <inheritdoc/>
        public void RunInTransactionGroup(Action action, string transactionGroupName, Document? document = null)
        {
            RunInTransactionGroup(action.ConvertToFunc(), transactionGroupName, document);
        }

        /// <inheritdoc />
        public T RunInTransaction<T>(Func<T> func, string transactionName, Document? document = null)
        {
            using var transaction = new Transaction(document ?? CurrentDocument, transactionName);
            try
            {
                transaction.Start();
                var result = func.Invoke();
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                if (transaction.GetStatus() != TransactionStatus.RolledBack)
                    transaction.RollBack();
                throw;
            }
        }

        /// <inheritdoc />
        public T RunInTransactionGroup<T>(Func<T> func, string transactionGroupName, Document? document = null)
        {
            using var transactionGroup = new TransactionGroup(document ?? CurrentDocument, transactionGroupName);
            try
            {
                transactionGroup.Start();
                var result = func.Invoke();
                transactionGroup.Assimilate();
                return result;
            }
            catch (Exception)
            {
                if (transactionGroup.GetStatus() != TransactionStatus.RolledBack)
                    transactionGroup.RollBack();
                throw;
            }
        }
    }
}