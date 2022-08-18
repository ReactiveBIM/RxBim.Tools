namespace RxBim.Tools
{
    /// <summary>
    /// <see cref="ITransactionContext"/> realisation for AutoCAD.
    /// </summary>
    public class TransactionContext : ITransactionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionContext"/> class.
        /// </summary>
        /// <param name="contextObject">Transaction context object.</param>
        public TransactionContext(object contextObject)
        {
            ContextObject = contextObject;
        }

        /// <inheritdoc />
        public object ContextObject { get; }
    }
}