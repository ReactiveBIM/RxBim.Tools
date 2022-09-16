namespace RxBim.Tools
{
    /// <summary>
    /// <see cref="ITransactionContextWrapper"/> realisation.
    /// </summary>
    public abstract class TransactionContextWrapper<T> : Wrapper<T>, ITransactionContextWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionContextWrapper{T}"/> class.
        /// </summary>
        /// <param name="contextObject">Transaction context object.</param>
        protected TransactionContextWrapper(T contextObject)
            : base(contextObject)
        {
        }
    }
}