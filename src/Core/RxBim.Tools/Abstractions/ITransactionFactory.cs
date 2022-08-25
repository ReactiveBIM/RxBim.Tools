namespace RxBim.Tools
{
    /// <summary>
    /// Factory for <see cref="ITransaction"/>.
    /// </summary>
    public interface ITransactionFactory
    {
        /// <summary>
        /// Returns new transaction.
        /// </summary>
        /// <param name="context">
        /// The context(document, drawing database) on which the transaction is executed.
        /// </param>
        /// <param name="name">Transaction name.</param>
        /// <typeparam name="T">Transaction context type.</typeparam>
        ITransaction CreateTransaction<T>(T context, string? name = null)
            where T : class, ITransactionContext;

        /// <summary>
        /// Returns new transaction group.
        /// </summary>
        /// <param name="context">
        /// The context(document, drawing database) on which the transaction group is executed.
        /// </param>
        /// <param name="name">Transaction group name.</param>
        /// <typeparam name="T">Transaction context type.</typeparam>
        ITransactionGroup CreateTransactionGroup<T>(T context, string? name = null)
            where T : class, ITransactionContext;

        /// <summary>
        /// Returns default transaction context.
        /// </summary>
        T GetDefaultContext<T>()
            where T : class, ITransactionContext;
    }
}