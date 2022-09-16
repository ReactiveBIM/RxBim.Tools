namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <inheritdoc cref="ITransactionWrapper" />
    [UsedImplicitly]
    internal class AutocadTransactionWrapper : AutocadTransactionWrapperBase
    {
        /// <inheritdoc />
        public AutocadTransactionWrapper(Transaction transaction)
        : base(transaction)
        {
        }
    }
}