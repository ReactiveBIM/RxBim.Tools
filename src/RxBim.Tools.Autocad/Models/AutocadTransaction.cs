namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <inheritdoc cref="RxBim.Tools.ITransaction" />
    [UsedImplicitly]
    internal class AutocadTransaction : AutocadTransactionBase
    {
        /// <inheritdoc />
        public AutocadTransaction(Transaction transaction, ITransactionContext context)
        : base(transaction, context)
        {
        }
    }
}