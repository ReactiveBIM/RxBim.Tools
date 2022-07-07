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
    }
}