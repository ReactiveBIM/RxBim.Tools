namespace RxBim.Tools.Revit.Abstractions
{
    using System;
    using Autodesk.Revit.DB;
    using CSharpFunctionalExtensions;
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
        /// <param name="document">
        /// The document in which the action is performed. If null, runs in the current document.
        /// </param>
        Result RunInTransaction(Action action, string transactionName, Document? document = null);

        /// <summary>
        /// Wraps an action in a transaction group and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction group.</param>
        /// <param name="transactionGroupName">Transaction group name.</param>
        /// <param name="document">The document in which the action is performed. If null, runs in the current document.</param>
        Result RunInTransactionGroup(Action action, string transactionGroupName, Document? document = null);

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="transactionName">Transaction name.</param>
        /// <param name="document">The document in which the action is performed. If null, runs in the current document.</param>
        Result RunInTransaction(Func<Result> func, string transactionName, Document? document = null);

        /// <summary>
        /// Wraps an action in a transaction group and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction group.</param>
        /// <param name="transactionGroupName">Transaction group name.</param>
        /// <param name="document">The document in which the action is performed. If null, runs in the current document.</param>
        Result RunInTransactionGroup(Func<Result> func, string transactionGroupName, Document? document = null);
    }
}