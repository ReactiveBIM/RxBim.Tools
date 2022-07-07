namespace RxBim.Tools
{
    /// <summary>
    /// Base transaction object.
    /// </summary>
    public interface ITransactionBase
    {
        /// <summary>
        /// Starts.
        /// </summary>
        void Start();

        /// <summary>
        /// Rolls back all changes.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Returns true if all changes have been rolled back. Otherwise, returns false.
        /// </summary>
        bool IsRolledBack();
    }
}