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
        public void RunInTransaction(
            Action<ITransactionContext> action,
            string? transactionName = null,
            ITransactionContext? transactionContext = null)
        {
            RunInTransaction(action.ToFunc(), transactionName, transactionContext);
        }

        /// <inheritdoc />
        public void RunInTransaction(
            Action<ITransaction, ITransactionContext> action,
            string? transactionName = null,
            ITransactionContext? transactionContext = null)
        {
            RunInTransaction(action.ToFunc(), transactionName, transactionContext);
        }

        /// <inheritdoc />
        public T RunInTransaction<T>(
            Func<ITransactionContext, T> func,
            string? transactionName = null,
            ITransactionContext? transactionContext = null)
        {
            return RunInTransaction((_, context) => func(context), transactionName, transactionContext);
        }

        /// <inheritdoc />
        public T RunInTransaction<T>(
            Func<ITransaction, ITransactionContext, T> func,
            string? transactionName = null,
            ITransactionContext? transactionContext = null)
        {
            var (transaction, context) = _transactionFactory.CreateTransaction(transactionContext, transactionName);
            using (transaction)
            {
                try
                {
                    transaction.Start();
                    var result = func.Invoke(transaction, context);
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
        }

        /// <inheritdoc />
        public void RunInTransactionGroup(
            Action<ITransactionContext> action,
            string transactionGroupName,
            ITransactionContext? transactionContext = null)
        {
            RunInTransactionGroup(action.ToFunc(), transactionGroupName, transactionContext);
        }

        /// <inheritdoc />
        public T RunInTransactionGroup<T>(
            Func<ITransactionContext, T> func,
            string transactionGroupName,
            ITransactionContext? transactionContext = null)
        {
            var (transactionGroup, context) =
                _transactionFactory.CreateTransactionGroup(transactionContext, transactionGroupName);

            using (transactionGroup)
            {
                try
                {
                    transactionGroup.Start();
                    var result = func.Invoke(context);
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
}