namespace RxBim.Tools.Autocad
{
    using System;

    /// <summary>
    /// Extensions for <see cref="ITransactionService"/> for <see cref="DocumentContext"/>.
    /// </summary>
    public static class DocumentTransactionServiceExtensions
    {
        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" />
        public static void RunInDocumentTransaction(
            this ITransactionService transactionService,
            Action action,
            string? name = null,
            DocumentContext? context = null)
        {
            transactionService.RunInTransaction(_ => action(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransaction}, string, T)" />
        public static void RunInDocumentTransaction(
            this ITransactionService transactionService,
            Action<ITransaction> action,
            string? name = null,
            DocumentContext? context = null)
        {
            transactionService.RunInTransaction((_, x) => action(x), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" />
        public static T RunInDocumentTransaction<T>(
            this ITransactionService transactionService,
            Func<T> func,
            string? name = null,
            DocumentContext? context = null)
        {
            return transactionService.RunInTransaction(_ => func(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransaction,T2}, string, T1)" />
        public static T RunInDocumentTransaction<T>(
            this ITransactionService transactionService,
            Func<ITransaction, T> func,
            string? name = null,
            DocumentContext? context = null)
        {
            return transactionService.RunInTransaction((_, x) => func(x), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" />
        public static void RunInDocumentTransactionGroup(
            this ITransactionService transactionService,
            Action action,
            string name,
            DocumentContext? transactionContext = null)
        {
            transactionService.RunInTransactionGroup(_ => action(), name, transactionContext);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroup}, string, T)" />
        public static void RunInDocumentTransactionGroup(
            this ITransactionService transactionService,
            Action<ITransactionGroup> action,
            string name,
            DocumentContext? transactionContext = null)
        {
            transactionService.RunInTransactionGroup((_, x) => action(x), name, transactionContext);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" />
        public static T RunInDocumentTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<T> func,
            string name,
            DocumentContext? context = null)
        {
            return transactionService.RunInTransactionGroup(_ => func(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroup,T2}, string, T1)" />
        public static T RunInDocumentTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<ITransactionGroup, T> func,
            string name,
            DocumentContext? context = null)
        {
            return transactionService.RunInTransactionGroup((_, x) => func(x), name, context);
        }
    }
}