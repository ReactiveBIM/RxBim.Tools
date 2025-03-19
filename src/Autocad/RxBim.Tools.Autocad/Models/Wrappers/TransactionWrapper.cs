namespace RxBim.Tools.Autocad;

using Autodesk.AutoCAD.DatabaseServices;
using JetBrains.Annotations;

/// <inheritdoc cref="ITransactionWrapper" />
[UsedImplicitly]
internal class TransactionWrapper(Transaction transaction) : TransactionWrapperBase(transaction);