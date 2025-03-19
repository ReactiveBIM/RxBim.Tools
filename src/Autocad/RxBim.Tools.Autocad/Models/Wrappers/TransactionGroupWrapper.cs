namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.DatabaseServices;

/// <inheritdoc cref="ITransactionGroupWrapper" />
internal class TransactionGroupWrapper(Transaction transaction)
    : TransactionWrapperBase(transaction), ITransactionGroupWrapper
{
    /// <inheritdoc />
    public void Assimilate() => Commit();
}