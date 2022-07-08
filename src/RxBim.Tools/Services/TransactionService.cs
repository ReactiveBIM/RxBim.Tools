namespace RxBim.Tools
{
    using System;

    /// <inheritdoc />
    internal class TransactionService : ITransactionService
    {
        private readonly ITransactionFactory _transactionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionService"/> class.
        /// </summary>
        /// <param name="transactionFactory"><see cref="ITransactionFactory"/>.</param>
        public TransactionService(ITransactionFactory transactionFactory)
        {
            _transactionFactory = transactionFactory;
        }

        /// <inheritdoc />
        public void RunInTransaction(Action action, string? transactionName = null, object? document = null)
        {
            RunInTransaction(action.ConvertToFunc(), transactionName, document);
        }

        /// <inheritdoc />
        public T RunInTransaction<T>(Func<T> func, string? transactionName = null, object? document = null)
        {
            using var transaction = _transactionFactory.GetTransaction(transactionName, document);
            try
            {
                transaction.Start();
                var result = func.Invoke();
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                if (!transaction.IsRolledBack())
                    transaction.RollBack();
                throw;
            }
        }

        /// <inheritdoc />
        public void RunInTransactionGroup(Action action, string transactionGroupName, object? document = null)
        {
            RunInTransactionGroup(action.ConvertToFunc(), transactionGroupName, document);
        }

        /// <inheritdoc />
        public T RunInTransactionGroup<T>(Func<T> func, string transactionGroupName, object? document = null)
        {
            using var transactionGroup = _transactionFactory.GetTransactionGroup(transactionGroupName, document);
            try
            {
                transactionGroup.Start();
                var result = func.Invoke();
                transactionGroup.Assimilate();
                return result;
            }
            catch (Exception)
            {
                if (!transactionGroup.IsRolledBack())
                    transactionGroup.RollBack();
                throw;
            }
        }
    }
}