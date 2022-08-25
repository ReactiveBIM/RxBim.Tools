namespace RxBim.Tools.Autocad
{
    using System;

    /// <summary>
    /// Extensions for <see cref="ITransactionService"/> for <see cref="DatabaseContext"/>.
    /// </summary>
    public static class DatabaseTransactionServiceExtensions
    {
        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" />
        public static void RunInDatabaseTransaction(
            this ITransactionService transactionService,
            Action action,
            string? name = null,
            DatabaseContext? context = null)
        {
            transactionService.RunInTransaction(_ => action(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransaction}, string, T)" />
        public static void RunInDatabaseTransaction(
            this ITransactionService transactionService,
            Action<ITransaction> action,
            string? name = null,
            DatabaseContext? context = null)
        {
            transactionService.RunInTransaction((_, x) => action(x), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" />
        public static T RunInDatabaseTransaction<T>(
            this ITransactionService transactionService,
            Func<T> func,
            string? name = null,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransaction(_ => func(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransaction,T2}, string, T1)" />
        public static T RunInDatabaseTransaction<T>(
            this ITransactionService transactionService,
            Func<ITransaction, T> func,
            string? name = null,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransaction((_, x) => func(x), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" />
        public static void RunInDatabaseTransactionGroup(
            this ITransactionService transactionService,
            Action action,
            string name,
            DatabaseContext? transactionContext = null)
        {
            transactionService.RunInTransactionGroup(_ => action(), name, transactionContext);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroup}, string, T)" />
        public static void RunInDatabaseTransactionGroup(
            this ITransactionService transactionService,
            Action<ITransactionGroup> action,
            string name,
            DatabaseContext? transactionContext = null)
        {
            transactionService.RunInTransactionGroup((_, x) => action(x), name, transactionContext);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" />
        public static T RunInDatabaseTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<T> func,
            string name,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransactionGroup(_ => func(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroup,T2}, string, T1)" />
        public static T RunInDatabaseTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<ITransactionGroup, T> func,
            string name,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransactionGroup((_, x) => func(x), name, context);
        }
    }
}