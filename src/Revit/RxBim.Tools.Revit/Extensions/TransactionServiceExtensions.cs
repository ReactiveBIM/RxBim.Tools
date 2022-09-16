namespace RxBim.Tools.Revit.Extensions
{
    using System;
    using Models;

    /// <summary>
    /// Extensions for <see cref="TransactionService"/>.
    /// </summary>
    public static class TransactionServiceExtensions
    {
        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" />
        public static void RunInTransaction(
            this ITransactionService transactionService,
            Action action,
            string? name = null,
            DocumentContextWrapper? context = null)
        {
            transactionService.RunInTransaction(_ => action(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransactionWrapper}, string, T)" />
        public static void RunInTransaction(
            this ITransactionService transactionService,
            Action<ITransactionWrapper> action,
            string? name = null,
            DocumentContextWrapper? context = null)
        {
            transactionService.RunInTransaction((_, x) => action(x), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" />
        public static T RunInTransaction<T>(
            this ITransactionService transactionService,
            Func<T> func,
            string? name = null,
            DocumentContextWrapper? context = null)
        {
            return transactionService.RunInTransaction(_ => func(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" />
        public static T RunInTransaction<T>(
            this ITransactionService transactionService,
            Func<ITransactionWrapper, T> func,
            string? name = null,
            DocumentContextWrapper? context = null)
        {
            return transactionService.RunInTransaction((_, x) => func(x), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" />
        public static void RunInTransactionGroup(
            this ITransactionService transactionService,
            Action action,
            string name,
            DocumentContextWrapper? transactionContext = null)
        {
            transactionService.RunInTransactionGroup(_ => action(), name, transactionContext);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroupWrapper}, string, T)" />
        public static void RunInTransactionGroup(
            this ITransactionService transactionService,
            Action<ITransactionGroupWrapper> action,
            string name,
            DocumentContextWrapper? transactionContext = null)
        {
            transactionService.RunInTransactionGroup((_, x) => action(x), name, transactionContext);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" />
        public static T RunInTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<T> func,
            string name,
            DocumentContextWrapper? context = null)
        {
            return transactionService.RunInTransactionGroup(_ => func(), name, context);
        }

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroupWrapper,T2}, string, T1)" />
        public static T RunInTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<ITransactionGroupWrapper, T> func,
            string name,
            DocumentContextWrapper? context = null)
        {
            return transactionService.RunInTransactionGroup((_, x) => func(x), name, context);
        }
    }
}