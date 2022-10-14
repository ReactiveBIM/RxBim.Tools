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
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the default context.
        /// </param>
        /// <typeparam name="T">The type of the transaction context.</typeparam>
        void RunInTransaction<T>(Action<T> action, string? name = null, T? context = null)
            where T : class, ITransactionContextWrapper;

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T}(Action{T}, string, T)" />
        void RunInTransaction<T>(Action<T, ITransactionWrapper> action, string? name = null, T? context = null)
            where T : class, ITransactionContextWrapper;

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="name">Transaction name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the default context.
        /// </param>
        /// <typeparam name="TContext">The type of the transaction context.</typeparam>
        /// <typeparam name="TRes">The type of the function result.</typeparam>
        TRes RunInTransaction<TContext, TRes>(Func<TContext, TRes> func, string? name = null, TContext? context = null)
            where TContext : class, ITransactionContextWrapper;

        /// <inheritdoc cref="ITransactionService.RunInTransaction{T1,T2}(Func{T1,ITransactionWrapper,T2}, string, T1)" />
        TRes RunInTransaction<TContext, TRes>(
            Func<TContext, ITransactionWrapper, TRes> func,
            string? name = null,
            TContext? context = null)
            where TContext : class, ITransactionContextWrapper;

        /// <summary>
        /// Wraps an action in a transaction group and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction group.</param>
        /// <param name="name">Transaction group name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the default context.
        /// </param>
        /// <typeparam name="T">The type of the transaction context.</typeparam>
        void RunInTransactionGroup<T>(Action<T> action, string name, T? context = null)
            where T : class, ITransactionContextWrapper;

        /// <inheritdoc cref="ITransactionService.RunInTransactionGroup{T}(Action{T}, string, T)" />
        void RunInTransactionGroup<T>(Action<T, ITransactionGroupWrapper> action, string name, T? context = null)
            where T : class, ITransactionContextWrapper;

        /// <summary>
        /// Wraps an action in a transaction group and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction group.</param>
        /// <param name="name">Transaction group name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the default context.
        /// </param>
        /// <typeparam name="TContext">The type of the transaction context.</typeparam>
        /// <typeparam name="TRes">The type of the function result.</typeparam>
        TRes RunInTransactionGroup<TContext, TRes>(Func<TContext, TRes> func, string name, TContext? context = null)
            where TContext : class, ITransactionContextWrapper;

        /// <summary>
        /// Wraps an action in a transaction group and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction group.</param>
        /// <param name="name">Transaction group name.</param>
        /// <param name="context">
        ///     The context(document, database, etc.) on which the action is performed. If null, runs in the default context.
        /// </param>
        /// <typeparam name="TContext">The type of the transaction context.</typeparam>
        /// <typeparam name="TRes">The type of the function result.</typeparam>
        TRes RunInTransactionGroup<TContext, TRes>(
            Func<TContext, ITransactionGroupWrapper, TRes> func,
            string name,
            TContext? context = null)
            where TContext : class, ITransactionContextWrapper;
    }
}