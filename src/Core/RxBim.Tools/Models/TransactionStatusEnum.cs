namespace RxBim.Tools
{
    using JetBrains.Annotations;

    /// <summary>
    /// Transaction statuses.
    /// </summary>
    [PublicAPI]
    public enum TransactionStatusEnum
    {
        /// <summary>
        /// Initial value, the transaction has not been started yet in this status.
        /// </summary>
        Uninitialized,

        /// <summary>
        /// Transaction has begun (until committed or rolled back).
        /// </summary>
        Started,

        /// <summary>
        /// Rolled back (aborted).
        /// </summary>
        RolledBack,

        /// <summary>
        /// Simply committed, ended an empty transaction, flushed all, or undo is disabled.
        /// </summary>
        Committed,

        /// <summary>
        /// Returned from error handling that took over managing the transaction.
        /// </summary>
        Pending,

        /// <summary>
        /// Error while committing or rolling back.
        /// </summary>
        Error,

        /// <summary>
        /// While still in error handling (internal status).
        /// </summary>
        Proceed,
    }
}