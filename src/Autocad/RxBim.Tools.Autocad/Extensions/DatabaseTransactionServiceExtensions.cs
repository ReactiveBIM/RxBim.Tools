namespace RxBim.Tools.Autocad
{
    using System;

    /// <summary>
    /// Extensions for <see cref="ITransactionService"/> for <see cref="DatabaseContext"/>.
    /// </summary>
    public static class DatabaseTransactionServiceExtensions
    {
        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" path="/summary" />
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" path="/param[@name='action']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" path="/param[@name='context']" /></param>
        public static void RunInDatabaseTransaction(
            this ITransactionService transactionService,
            Action action,
            DatabaseContext? context = null)
        {
            transactionService.RunInTransaction(_ => action(), null, context);
        }

        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransaction}, string, T)" path="/summary" />
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransaction}, string, T)" path="/param[@name='action']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T, ITransaction}, string, T)" path="/param[@name='context']" /></param>
        public static void RunInDatabaseTransaction(
            this ITransactionService transactionService,
            Action<ITransaction> action,
            DatabaseContext? context = null)
        {
            transactionService.RunInTransaction((_, x) => action(x), null, context);
        }

        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/summary" />
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='func']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='context']" /></param>
        /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
        public static T RunInDatabaseTransaction<T>(
            this ITransactionService transactionService,
            Func<T> func,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransaction(_ => func(), null, context);
        }

        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransaction,T2}, string, T1)" path="/summary" />
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransaction,T2}, string, T1)" path="/param[@name='func']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransaction,T2}, string, T1)" path="/param[@name='context']" /></param>
        /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransaction,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
        public static T RunInDatabaseTransaction<T>(
            this ITransactionService transactionService,
            Func<ITransaction, T> func,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransaction((_, x) => func(x), null, context);
        }

        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" path="/summary" />
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" path="/param[@name='action']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" path="/param[@name='context']" /></param>
        public static void RunInDatabaseTransactionGroup(
            this ITransactionService transactionService,
            Action action,
            DatabaseContext? context = null)
        {
            transactionService.RunInTransactionGroup(_ => action(), string.Empty, context);
        }

        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroup}, string, T)"  path="/summary" />
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="action"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroup}, string, T)" path="/param[@name='action']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T,ITransactionGroup}, string, T)" path="/param[@name='context']" /></param>
        public static void RunInDatabaseTransactionGroup(
            this ITransactionService transactionService,
            Action<ITransactionGroup> action,
            DatabaseContext? context = null)
        {
            transactionService.RunInTransactionGroup((_, x) => action(x), string.Empty, context);
        }

        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/summary" />
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='func']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/param[@name='context']" /></param>
        /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
        public static T RunInDatabaseTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<T> func,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransactionGroup(_ => func(), string.Empty, context);
        }

        /// <summary>
        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroup,T2}, string, T1)" path="/summary"/>
        /// </summary>
        /// <param name="transactionService"><see cref="ITransactionService"/> object.</param>
        /// <param name="func"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroup,T2}, string, T1)" path="/param[@name='func']" /></param>
        /// <param name="context"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroup,T2}, string, T1)" path="/param[@name='context']" /></param>
        /// <typeparam name="T"><inheritdoc cref="ITransactionService.RunInTransactionGroup{T1,T2}(Func{T1,ITransactionGroup,T2}, string, T1)" path="/typeparam[@name='TRes']" /></typeparam>
        public static T RunInDatabaseTransactionGroup<T>(
            this ITransactionService transactionService,
            Func<ITransactionGroup, T> func,
            DatabaseContext? context = null)
        {
            return transactionService.RunInTransactionGroup((_, x) => func(x), string.Empty, context);
        }
    }
}