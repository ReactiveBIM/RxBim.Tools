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

        /// <summary>
        /// Returns the object of the currently running transaction as a CAD object.
        /// </summary>
        /// <typeparam name="T">The type of the transaction object specified in the CAD.</typeparam>
        /// <returns></returns>
        T GetCadTransaction<T>()
            where T : class;
    }
}