namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.DatabaseServices;
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
        /// <param name="database">
        /// The database of the drawing in which the action is performed.
        /// If null, runs in the current drawing database.
        /// </param>
        void RunInTransaction(Action action, Database? database = null);

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="database">
        /// The database of the drawing in which the action is performed.
        /// If null, runs in the current drawing database.
        /// </param>
        /// <typeparam name="T">The type of the function result.</typeparam>
        T RunInTransaction<T>(Func<T> func, Database? database = null);

        /// <summary>
        /// Wraps an action in a transaction and executes it.
        /// </summary>
        /// <param name="action">An action to be executed within a transaction.</param>
        /// <param name="database">
        /// The database of the drawing in which the action is performed.
        /// If null, runs in the current drawing database.
        /// </param>
        void RunInTransaction(Action<Transaction> action, Database? database = null);

        /// <summary>
        /// Wraps a function in a transaction and executes it. Returns the result of the function execution.
        /// </summary>
        /// <param name="func">A function to be executed within a transaction.</param>
        /// <param name="database">
        /// The database of the drawing in which the action is performed.
        /// If null, runs in the current drawing database.
        /// </param>
        /// <typeparam name="T">The type of the function result.</typeparam>
        T RunInTransaction<T>(Func<Transaction, T> func, Database? database = null);
    }
}