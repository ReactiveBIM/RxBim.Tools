namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <inheritdoc cref="RxBim.Tools.ITransaction" />
    [UsedImplicitly]
    internal class AutocadTransaction : AutocadTransactionBase, ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutocadTransaction"/> class.
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> instance.</param>
        public AutocadTransaction(Transaction transaction)
        : base(transaction)
        {
        }
    }
}