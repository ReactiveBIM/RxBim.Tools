namespace RxBim.Tools
{
    /// <summary>
    /// Transaction group.
    /// </summary>
    public interface ITransactionGroup : ITransactionBase
    {
        /// <summary>
        /// Assimilates all inner transactions.
        /// </summary>
        void Assimilate();
    }
}