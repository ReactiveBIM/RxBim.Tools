namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <inheritdoc cref="ITransactionWrapper" />
    [UsedImplicitly]
    internal class TransactionWrapper : TransactionWrapperBase
    {
        /// <inheritdoc />
        public TransactionWrapper(Transaction transaction)
        : base(transaction)
        {
        }
    }
}