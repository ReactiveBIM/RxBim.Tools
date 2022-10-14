namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc cref="ITransactionGroupWrapper" />
    internal class TransactionGroupWrapper : TransactionWrapperBase, ITransactionGroupWrapper
    {
        /// <inheritdoc />
        public TransactionGroupWrapper(Transaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc />
        public void Assimilate() => Commit();
    }
}