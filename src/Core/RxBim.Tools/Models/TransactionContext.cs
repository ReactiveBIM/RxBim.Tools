namespace RxBim.Tools
{
    /// <summary>
    /// <see cref="ITransactionContext"/> realisation for AutoCAD.
    /// </summary>
    public abstract class TransactionContext<T> : ITransactionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionContext{T}"/> class.
        /// </summary>
        /// <param name="contextObject">Transaction context object.</param>
        protected TransactionContext(T contextObject)
        {
            ContextObject = contextObject;
        }

        /// <summary>
        /// Transaction context object.
        /// </summary>
        public T ContextObject { get; }
    }
}