namespace RxBim.Tools
{
    /// <summary>
    /// Transaction object.
    /// </summary>
    public interface ITransaction : ITransactionBase
    {
        /// <summary>
        /// Commits all changes.
        /// </summary>
        void Commit();
    }
}