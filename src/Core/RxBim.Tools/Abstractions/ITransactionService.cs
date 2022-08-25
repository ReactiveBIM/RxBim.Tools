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
        /// <param name="name">Transaction name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="T">The type of the transaction context.</typeparam>
        void RunInTransaction<T>(Action<T> action, string? name = null, T? context = null)
            where T : class, ITransactionContext;

        /// <summary>
        /// Wraps an action in a transaction and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction.</param>
        /// <param name="name">Transaction name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="T">The type of the transaction context.</typeparam>
        void RunInTransaction<T>(Action<T, ITransaction> action, string? name = null, T? context = null)
            where T : class, ITransactionContext;

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="name">Transaction name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="TContext">The type of the transaction context.</typeparam>
        /// <typeparam name="TRes">The type of the function result.</typeparam>
        TRes RunInTransaction<TContext, TRes>(Func<TContext, TRes> func, string? name = null, TContext? context = null)
            where TContext : class, ITransactionContext;

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="name">Transaction name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="TContext">The type of the transaction context.</typeparam>
        /// <typeparam name="TRes">The type of the function result.</typeparam>
        TRes RunInTransaction<TContext, TRes>(
            Func<TContext, ITransaction, TRes> func,
            string? name = null,
            TContext? context = null)
            where TContext : class, ITransactionContext;

        /// <summary>
        /// Wraps an action in a transaction group and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction group.</param>
        /// <param name="name">Transaction group name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="T">The type of the transaction context.</typeparam>
        void RunInTransactionGroup<T>(Action<T> action, string name, T? context = null)
            where T : class, ITransactionContext;

        /// <summary>
        /// Wraps an action in a transaction group and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction group.</param>
        /// <param name="name">Transaction group name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="TContext">The type of the transaction context.</typeparam>
        /// <typeparam name="TRes">The type of the function result.</typeparam>
        TRes RunInTransactionGroup<TContext, TRes>(Func<TContext, TRes> func, string name, TContext? context = null)
            where TContext : class, ITransactionContext;

        /// <summary>
        /// Wraps an action in a transaction group and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction group.</param>
        /// <param name="name">Transaction group name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the current target.
        /// </param>
        /// <typeparam name="TContext">The type of the transaction context.</typeparam>
        /// <typeparam name="TRes">The type of the function result.</typeparam>
        TRes RunInTransactionGroup<TContext, TRes>(
            Func<TContext, ITransactionGroup, TRes> func,
            string name,
            TContext? context = null)
            where TContext : class, ITransactionContext;
    }
}