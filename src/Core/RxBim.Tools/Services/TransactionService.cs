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
        public void RunInTransaction<T>(Action<T> action, string? name = null, T? context = null)
            where T : class, ITransactionContext
        {
            RunInTransaction(action.ToFunc(), name, context);
        }

        /// <inheritdoc />
        public void RunInTransaction<T>(Action<T, ITransaction> action, string? name = null, T? context = null)
            where T : class, ITransactionContext
        {
            RunInTransaction(action.ToFunc(), name, context);
        }

        /// <inheritdoc />
        public TRes RunInTransaction<TContext, TRes>(
            Func<TContext, TRes> func,
            string? name = null,
            TContext? context = null)
            where TContext : class, ITransactionContext
        {
            var transactionContext = context ?? _transactionFactory.GetDefaultContext<TContext>();
            return RunInTransaction((_, _) => func(transactionContext), name, transactionContext);
        }

        /// <inheritdoc />
        public TRes RunInTransaction<TContext, TRes>(
            Func<TContext, ITransaction, TRes> func,
            string? name = null,
            TContext? context = null)
            where TContext : class, ITransactionContext
        {
            var transactionContext = context ?? _transactionFactory.GetDefaultContext<TContext>();
            using var transaction = _transactionFactory.CreateTransaction(transactionContext, name);
            try
            {
                transaction.Start();
                var result = func.Invoke(transactionContext, transaction);
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
        public void RunInTransactionGroup<T>(
            Action<T> action,
            string name,
            T? transactionContext = null)
            where T : class, ITransactionContext
        {
            RunInTransactionGroup(action.ToFunc(), name, transactionContext);
        }

        /// <inheritdoc />
        public TRes RunInTransactionGroup<TContext, TRes>(
            Func<TContext, TRes> func,
            string name,
            TContext? context = null)
            where TContext : class, ITransactionContext
        {
            var transactionContext = context ?? _transactionFactory.GetDefaultContext<TContext>();
            return RunInTransactionGroup((_, _) => func(transactionContext), name, transactionContext);
        }

        /// <inheritdoc />
        public TRes RunInTransactionGroup<TContext, TRes>(
            Func<TContext, ITransactionGroup, TRes> func,
            string name,
            TContext? context = null)
            where TContext : class, ITransactionContext
        {
            var transactionContext = _transactionFactory.GetDefaultContext<TContext>();
            using var transactionGroup =
                _transactionFactory.CreateTransactionGroup(transactionContext, name);
            try
            {
                transactionGroup.Start();
                var result = func.Invoke(transactionContext, transactionGroup);
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