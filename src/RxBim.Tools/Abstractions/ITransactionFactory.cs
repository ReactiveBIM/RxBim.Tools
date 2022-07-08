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
        /// <param name="transactionName">Transaction name.</param>
        /// <param name="document">The owner(document, drawing database) on which the transaction is executed.</param>
        ITransaction GetTransaction(string? transactionName = null, object? document = null);

        /// <summary>
        /// Returns transaction group.
        /// </summary>
        /// <param name="transactionGroupName">Transaction group name.</param>
        /// <param name="document">The owner(document, drawing database) on which the transaction group is executed.</param>
        ITransactionGroup GetTransactionGroup(string? transactionGroupName = null, object? document = null);
    }
}