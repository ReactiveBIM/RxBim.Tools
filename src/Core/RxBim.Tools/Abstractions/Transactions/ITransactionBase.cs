namespace RxBim.Tools
{
    using System;

    /// <summary>
    /// Base transaction object.
    /// </summary>
    public interface ITransactionBase : IDisposable, IWrapper
    {
        /// <summary>
        /// Starts.
        /// </summary>
        void Start();

        /// <summary>
        /// Rolls back all changes.
        /// </summary>
        void RollBack();

        /// <summary>
        /// Returns true if all changes have been rolled back. Otherwise, returns false.
        /// </summary>
        bool IsRolledBack();
    }
}