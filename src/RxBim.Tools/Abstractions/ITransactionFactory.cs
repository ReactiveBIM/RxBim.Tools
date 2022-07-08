namespace RxBim.Tools
{
    /// <summary>
    /// Factory for <see cref="ITransaction"/>.
    /// </summary>
    public interface ITransactionFactory
    {
        /// <summary>
        /// Returns transaction.
        /// </summary>
        /// <param name="transactionContext">
        /// The context(document, drawing database) on which the transaction is executed.
        /// </param>
        /// <param name="transactionName">Transaction name.</param>
        ITransaction GetTransaction(object? transactionContext = null, string? transactionName = null);

        /// <summary>
        /// Returns transaction group.
        /// </summary>
        /// <param name="document">
        /// The context(document, drawing database) on which the transaction group is executed.
        /// </param>
        /// <param name="transactionGroupName">Transaction group name.</param>
        ITransactionGroup GetTransactionGroup(object? document = null, string? transactionGroupName = null);
    }
}