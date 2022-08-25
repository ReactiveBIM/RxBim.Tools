namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc cref="RxBim.Tools.ITransactionGroup" />
    internal class AutocadTransactionGroup : AutocadTransactionBase, ITransactionGroup
    {
        /// <inheritdoc />
        public AutocadTransactionGroup(Transaction transaction, ITransactionContext context)
            : base(transaction, context)
        {
        }

        /// <inheritdoc />
        public void Assimilate() => Commit();
    }
}