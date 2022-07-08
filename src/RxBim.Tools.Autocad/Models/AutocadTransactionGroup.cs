namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc cref="RxBim.Tools.ITransactionGroup" />
    internal class AutocadTransactionGroup : AutocadTransactionBase, ITransactionGroup
    {
        /// <inheritdoc />
        public AutocadTransactionGroup(Transaction transaction)
            : base(transaction)
        {
        }

        /// <inheritdoc />
        public void Assimilate() => Commit();
    }
}