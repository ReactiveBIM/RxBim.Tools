namespace RxBim.Tools
{
    using System;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
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
        public void RunInTransaction(Action action, string? transactionName = null, object? transactionContext = null)
        {
            RunInTransaction(action.ToFunc(), transactionName, transactionContext);
        }

        /// <inheritdoc />
        public T RunInTransaction<T>(Func<T> func, string? transactionName = null, object? transactionContext = null)
        {
            using var transaction = _transactionFactory.GetTransaction(transactionContext, transactionName);
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
        public void RunInTransactionGroup(Action action, string transactionGroupName, object? transactionContext = null)
        {
            RunInTransactionGroup(action.ToFunc(), transactionGroupName, transactionContext);
        }

        /// <inheritdoc />
        public T RunInTransactionGroup<T>(Func<T> func, string transactionGroupName, object? transactionContext = null)
        {
            using var transactionGroup = _transactionFactory.GetTransactionGroup(transactionContext, transactionGroupName);
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