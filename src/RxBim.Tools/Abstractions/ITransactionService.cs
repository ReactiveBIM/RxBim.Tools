namespace RxBim.Tools
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Transaction service.
    /// </summary>
    [PublicAPI]
    public interface ITransactionService
    {
        /// <summary>
        /// Wraps an action in a transaction and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction.</param>
        /// <param name="transactionName">Transaction name.</param>
        /// <param name="transactionContext">
        /// The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        void RunInTransaction(
            Action<ITransactionContext> action,
            string? transactionName = null,
            ITransactionContext? transactionContext = null);

        /// <summary>
        /// Wraps an action in a transaction and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction.</param>
        /// <param name="transactionName">Transaction name.</param>
        /// <param name="transactionContext">
        /// The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        void RunInTransaction(
            Action<ITransaction, ITransactionContext> action,
            string? transactionName = null,
            ITransactionContext? transactionContext = null);

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="transactionName">Transaction name.</param>
        /// <param name="transactionContext">
        /// The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="T">The type of the function result.</typeparam>
        T RunInTransaction<T>(
            Func<ITransactionContext, T> func,
            string? transactionName = null,
            ITransactionContext? transactionContext = null);

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="transactionName">Transaction name.</param>
        /// <param name="transactionContext">
        /// The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="T">The type of the function result.</typeparam>
        T RunInTransaction<T>(
            Func<ITransaction, ITransactionContext, T> func,
            string? transactionName = null,
            ITransactionContext? transactionContext = null);

        /// <summary>
        /// Wraps an action in a transaction group and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction group.</param>
        /// <param name="transactionGroupName">Transaction group name.</param>
        /// <param name="transactionContext">
        /// The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        void RunInTransactionGroup(
            Action<ITransactionContext> action,
            string transactionGroupName,
            ITransactionContext? transactionContext = null);

        /// <summary>
        /// Wraps an action in a transaction group and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction group.</param>
        /// <param name="transactionGroupName">Transaction group name.</param>
        /// <param name="transactionContext">
        /// The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        T RunInTransactionGroup<T>(
            Func<ITransactionContext, T> func,
            string transactionGroupName,
            ITransactionContext? transactionContext = null);
    }
}