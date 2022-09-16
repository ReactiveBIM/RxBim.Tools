namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc cref="ITransactionGroupWrapper" />
    internal class AutocadTransactionGroupWrapper : AutocadTransactionWrapperBase, ITransactionGroupWrapper
    {
        /// <inheritdoc />
        public AutocadTransactionGroupWrapper(Transaction transaction, ITransactionContextWrapper context)
            : base(transaction)
        {
        }

        /// <inheritdoc />
        public void Assimilate() => Commit();
    }
}