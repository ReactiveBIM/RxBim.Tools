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
        /// <param name="transactionContext">
        /// The context(document, drawing database) on which the transaction is executed.
        /// </param>
        /// <param name="transactionName">Transaction name.</param>
        ITransaction CreateTransaction(object? transactionContext = null, string? transactionName = null);

        /// <summary>
        /// Returns new transaction group.
        /// </summary>
        /// <param name="document">
        /// The context(document, drawing database) on which the transaction group is executed.
        /// </param>
        /// <param name="transactionGroupName">Transaction group name.</param>
        ITransactionGroup CreateTransactionGroup(object? document = null, string? transactionGroupName = null);
    }
}