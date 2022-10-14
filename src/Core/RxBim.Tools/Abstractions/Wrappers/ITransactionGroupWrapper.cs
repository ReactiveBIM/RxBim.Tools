namespace RxBim.Tools
{
    /// <summary>
    /// Transaction group wrapper.
    /// </summary>
    public interface ITransactionGroupWrapper : ITransactionWrapperBase
    {
        /// <summary>
        /// Assimilates all inner transactions.
        /// </summary>
        void Assimilate();
    }
}