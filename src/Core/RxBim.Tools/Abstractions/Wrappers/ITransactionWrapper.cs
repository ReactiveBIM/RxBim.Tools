namespace RxBim.Tools
{
    /// <summary>
    /// Transaction wrapper.
    /// </summary>
    public interface ITransactionWrapper : ITransactionWrapperBase
    {
        /// <summary>
        /// Commits all changes.
        /// </summary>
        void Commit();
    }
}